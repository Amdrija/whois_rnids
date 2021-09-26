using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Application.UseCases.GetPopularDomains;

namespace RNIDS.WHOIS.Application.UseCases.GetRandomDomain
{
    public class GetRandomDomainUseCase : IUseCase<GetRandomDomainRequest, GetRandomDomainResponse>
    {
        private readonly IDomainRepository domainRepository;

        public GetRandomDomainUseCase(IDomainRepository domainRepository)
        {
            this.domainRepository = domainRepository;
        }

        public async Task<GetRandomDomainResponse> ExecuteAsync(GetRandomDomainRequest request)
        {
            return new GetRandomDomainResponse()
            {
                Domain = await this.domainRepository.GetRandomAsync()
            };
        }
    }
}