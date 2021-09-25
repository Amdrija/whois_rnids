using System.Threading.Tasks;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.Application.Interfaces.Repositories
{
    public interface IWhoIsInformationRepository
    {
        public Task<Core.Models.Domain> GetAsync(string domainName, string whoisProvider);
    }
}