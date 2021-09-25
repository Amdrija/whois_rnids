using System.Threading.Tasks;
using RNIDS.WHOIS.Domain.Models;

namespace RNIDS.WHOIS.Application.Interfaces.Repositories
{
    public interface IWhoIsInformationRepository
    {
        public Task<string> GetAsync(string domainName, string whoisProvider);
    }
}