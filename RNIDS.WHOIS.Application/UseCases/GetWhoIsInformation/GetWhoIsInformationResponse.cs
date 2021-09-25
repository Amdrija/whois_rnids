using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.Application.UseCases.GetWhoIsInformation
{
    public class GetWhoIsInformationResponse
    {
        public  Core.Models.Domain Information { get; init; }
    }
}