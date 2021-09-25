using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Domain.Services;

namespace RNIDS.WHOIS.Application.UseCases.GetWhoIsInformation
{
    public class GetWhoIsInformationUseCase : IUseCase<GetWhoIsInformationRequest, GetWhoIsInformationResponse>
    {
        private readonly IWhoIsInformationRepository repository;
        private readonly WhoIsProviderRepository providerRepository;

        public GetWhoIsInformationUseCase(
            IWhoIsInformationRepository repository,
            WhoIsProviderRepository providerRepository)
        {
            this.repository = repository;
            this.providerRepository = providerRepository;
        }

        public async Task<GetWhoIsInformationResponse> ExecuteAsync(GetWhoIsInformationRequest request)
        {
            string providerName = providerRepository.GetProvider(request.Domain);

            return new GetWhoIsInformationResponse()
            {
                Information =
                    await this.repository.GetAsync(request.Domain.ToString().GetPuny(), providerName.GetPuny())
            };
        }
    }
}