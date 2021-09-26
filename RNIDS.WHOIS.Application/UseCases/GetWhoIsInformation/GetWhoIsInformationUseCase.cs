using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.Core.Services;

namespace RNIDS.WHOIS.Application.UseCases.GetWhoIsInformation
{
    public class GetWhoIsInformationUseCase : IUseCase<GetWhoIsInformationRequest, GetWhoIsInformationResponse>
    {
        private readonly IWhoIsInformationRepository repository;
        private readonly WhoIsProviderRepository providerRepository;
        private readonly IDomainRepository domainRepository;

        public GetWhoIsInformationUseCase(
            IWhoIsInformationRepository repository,
            WhoIsProviderRepository providerRepository,
            IDomainRepository domainRepository)
        {
            this.repository = repository;
            this.providerRepository = providerRepository;
            this.domainRepository = domainRepository;
        }

        public async Task<GetWhoIsInformationResponse> ExecuteAsync(GetWhoIsInformationRequest request)
        {
            string providerName = providerRepository.GetProvider(request.Domain);

            Domain domain = await this.domainRepository.GetAsync(request.Domain.ToString().ToLower());

            if (domain != null)
            {
                domain.IncrementSearch();
                await this.domainRepository.UpdateAsync(domain);
            }
            else
            {
                domain = await this.repository.GetAsync(request.Domain.ToString().ToLower(), providerName);
                await this.domainRepository.CreateAsync(domain);
            }

            return new GetWhoIsInformationResponse()
            {
                Information = domain
            };
        }
    }
}