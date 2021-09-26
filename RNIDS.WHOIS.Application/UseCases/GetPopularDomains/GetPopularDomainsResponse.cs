using System.Collections;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.Application.UseCases.GetPopularDomains
{
    public class GetPopularDomainsResponse
    {
        public IEnumerable<Domain> Domains { get; init; }
    }
}