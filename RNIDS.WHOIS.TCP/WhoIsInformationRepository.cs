using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Core.Models;
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
                    BufferedStream bufferedStream = new BufferedStream(networkStream);
                    StreamWriter streamWriter = new StreamWriter(bufferedStream);

                    await streamWriter.WriteLineAsync(domainName);
                    await streamWriter.FlushAsync();
                    
                    StreamReader streamReaderReceive = new StreamReader(bufferedStream);

                    while (!streamReaderReceive.EndOfStream)
                        responseBuilder.AppendLine(await streamReaderReceive.ReadLineAsync());
                    
                    streamWriter.Close();
                    bufferedStream.Close();
                }
            }

            string response = responseBuilder.ToString();
            Dictionary<string, string> whoIsDictionary = this.GetWhoIsDictionary(response);
            
            return this.factory.Create(whoisProvider).Convert(whoIsDictionary, response);
        }

        private Dictionary<string, string> GetWhoIsDictionary(string response)
        {
            string parsedResponse = response.Replace(":" + Environment.NewLine, ":");
            
            IEnumerable<string> keyValuePairs = parsedResponse.Split(Environment.NewLine).Where(s => s.Contains(":"));
            return keyValuePairs.Aggregate(new Dictionary<string, string>(), (dictionary, s) =>
            {
                string[] keyValuePair = s.Split(":");
                if (!dictionary.ContainsKey(keyValuePair[0]))
                {
                    dictionary.Add(keyValuePair[0], string.Join(":", keyValuePair[1..]));
                }

                return dictionary;
            });
        }
    }
}