using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Interfaces.Repositories;

namespace RNIDS.WHOIS.TCP
{
    public class WhoIsInformationRepository : IWhoIsInformationRepository
    {
        public async Task<string> GetAsync(string domainName, string whoisProvider)
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

            return responseBuilder.ToString();
        }
    }
}