using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.Interfaces.Repositories;

namespace RNIDS.WHOIS.Application.UseCases.ClearOldDomains
{
    public class ClearOldDomainsUseCase : IUseCase<ClearOldDomainsRequest>
    {
        private readonly IDomainRepository domainRepository;

        public ClearOldDomainsUseCase(IDomainRepository domainRepository)
        {
            this.domainRepository = domainRepository;
        }

        public Task ExecuteAsync(ClearOldDomainsRequest request)
        {
            return this.domainRepository.ClearOldAsync(request.BeforeDays);
        }
    }
}