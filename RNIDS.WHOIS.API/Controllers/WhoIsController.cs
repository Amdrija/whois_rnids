using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.Interfaces.Services;
using RNIDS.WHOIS.Application.UseCases;
using RNIDS.WHOIS.Application.UseCases.CreateEmailReminder;
using RNIDS.WHOIS.Application.UseCases.GetPopularDomains;
using RNIDS.WHOIS.Application.UseCases.GetRandomDomain;
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
        
        [HttpGet("random")]
        public async Task<DomainViewModel> Get(
            [FromServices] IUseCase<GetRandomDomainRequest, GetRandomDomainResponse> useCase)
        {
            GetRandomDomainResponse response = await useCase.ExecuteAsync(new());
            return new DomainViewModel(response.Domain);
        }
        
        [HttpGet("popular")]
        public async Task<IEnumerable<DomainViewModel>> Get(
            [FromServices] IUseCase<GetPopularDomainsRequest, GetPopularDomainsResponse> useCase)
        {
            GetPopularDomainsResponse response = await useCase.ExecuteAsync(new());
            return response.Domains.Select(d => new DomainViewModel(d));
        }

        [HttpGet("email")]
        public Task SendEmail(
            [FromQuery] CreateEmailReminderRequest request,
            [FromServices] IUseCase<CreateEmailReminderRequest> useCase)
        {
            return useCase.ExecuteAsync(request);
        }
    }
}