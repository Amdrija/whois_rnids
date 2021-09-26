using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.Interfaces.Repositories;

namespace RNIDS.WHOIS.Application.UseCases.GetPopularDomains
{
    public class GetPopularDomainsUseCase : IUseCase<GetPopularDomainsRequest, GetPopularDomainsResponse>
    {
        private readonly IDomainRepository domainRepository;

        public GetPopularDomainsUseCase(IDomainRepository domainRepository)
        {
            this.domainRepository = domainRepository;
        }

        public async Task<GetPopularDomainsResponse> ExecuteAsync(GetPopularDomainsRequest request)
        {
            return new GetPopularDomainsResponse()
            {
                Domains = await this.domainRepository.GetPopularAsync()
            };
        }
    }
}