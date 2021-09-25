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
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    StreamWriter streamWriter = new StreamWriter(networkStream);

                    await streamWriter.WriteLineAsync(domainName);
                    streamWriter.Flush();
                    
                    StreamReader streamReaderReceive = new StreamReader(networkStream);

                    while (!streamReaderReceive.EndOfStream)
                        responseBuilder.AppendLine(await streamReaderReceive.ReadLineAsync());
                    
                    streamWriter.Close();
                }
            }

            string response = responseBuilder.ToString();
            Dictionary<string, string> whoIsDictionary = WhoIsResponseParser.GetWhoIsDictionary(response);
            
            return this.factory.Create(whoisProvider).Convert(whoIsDictionary, response);
        }
    }
}