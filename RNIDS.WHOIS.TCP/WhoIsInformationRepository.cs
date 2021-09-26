using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;
using RNIDS.WHOIS.TCP.WhoIsInformationConversion;

namespace RNIDS.WHOIS.TCP
{
    public class WhoIsInformationRepository : IWhoIsInformationRepository
    {
        private readonly WhoIsConversionStrategyFactory factory;
        private const int BUFFER_SIZE = 2048;

        public WhoIsInformationRepository(WhoIsConversionStrategyFactory factory)
        {
            this.factory = factory;
        }

        public async Task<Core.Models.Domain> GetAsync(string domainName, string whoisProvider)
        {
            StringBuilder responseBuilder = new StringBuilder();

            using (TcpClient tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync(whoisProvider, 43);
                await using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    Byte[] payload = Encoding.ASCII.GetBytes(domainName + "\r\n");
                    await networkStream.WriteAsync(payload, 0, payload.Length);
                    await networkStream.FlushAsync();

                    int bytesRead = 0;
                    do
                    {
                        byte[] bytes = new byte[BUFFER_SIZE];

                        bytesRead = await networkStream.ReadAsync(bytes, 0, (int) BUFFER_SIZE);
                        responseBuilder.Append(Encoding.UTF8.GetString(bytes[..(bytesRead - 1)]));
                    } while (bytesRead == BUFFER_SIZE);
                }
            }

            string response = responseBuilder.ToString();
            
            return this.factory.Create(whoisProvider).Convert(response);
        }
    }
}