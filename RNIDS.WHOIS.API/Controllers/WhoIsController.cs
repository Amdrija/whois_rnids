using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.UseCases;
using RNIDS.WHOIS.Application.UseCases.GetWhoIsInformation;

namespace RNIDS.WHOIS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhoIsController : ControllerBase
    {
        [HttpGet]
        public Task<GetWhoIsInformationResponse> Get(
            [FromQuery] GetWhoIsInformationRequest request,
            [FromServices] IUseCase<GetWhoIsInformationRequest, GetWhoIsInformationResponse> useCase)
        {
            return useCase.ExecuteAsync(request);
        }
    }
}