using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.UseCases;
using RNIDS.WHOIS.Application.UseCases.GetWhoIsInformation;
using RNIDS.WHOIS.ViewModels;

namespace RNIDS.WHOIS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhoIsController : ControllerBase
    {
        [HttpGet]
        public async Task<DomainViewModel> Get(
            [FromQuery] GetWhoIsInformationRequest request,
            [FromServices] IUseCase<GetWhoIsInformationRequest, GetWhoIsInformationResponse> useCase)
        {
            GetWhoIsInformationResponse response = await useCase.ExecuteAsync(request);
            return new DomainViewModel(response.Information);
        }
    }
}