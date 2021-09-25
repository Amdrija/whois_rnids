using System.Threading.Tasks;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.Application.Interfaces.Repositories
{
    public interface IDomainRepository
    {
        public Task<Domain> GetAsync(string domainName);

        public Task CreateAsync(Domain domain);

        public Task DeleteAsync(Domain domain);

        public Task UpdateAsync(Domain domain);
    }
}