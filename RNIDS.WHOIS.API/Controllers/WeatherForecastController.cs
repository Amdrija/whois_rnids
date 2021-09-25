using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.UseCases;

namespace RNIDS.WHOIS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public Task<TestResponse> Get(
            [FromQuery] TestRequest request,
            [FromServices] IUseCase<TestRequest, TestResponse> useCase)
        {
            return useCase.ExecuteAsync(request);
        }
    }
}