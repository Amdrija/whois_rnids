using System;

namespace RNIDS.WHOIS.Application.UseCases.GetWhoIsInformation
{
    public class GetWhoIsInformationRequest
    {
        public Uri Domain { get; init; }
    }
}