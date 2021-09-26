using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Exception;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.Core.Services
{
    public class WhoIsProviderRepository
    {
        private readonly Dictionary<string, string> whoIsProviders;
        
        public WhoIsProviderRepository()
        {
            this.whoIsProviders = new Dictionary<string, string>
            {
                {".rs", WhoIsProviders.RNIDS},
                {".срб", WhoIsProviders.RNIDS},
                {".ru", WhoIsProviders.TCINET},
                {".рф", WhoIsProviders.TCINET},
                {".mk", WhoIsProviders.MARNET},
                {".мкд", WhoIsProviders.MARNET},
                {".org", WhoIsProviders.PUBLIC_INTEREST_REGISTRY},
                {".орг", WhoIsProviders.PUBLIC_INTEREST_REGISTRY},
                {".com", WhoIsProviders.VERISIGN_GRS},
                {".ком", WhoIsProviders.NIC_KOM},
                {".net", WhoIsProviders.VERISIGN_GRS},
                {".uk", WhoIsProviders.NIC_UK},
                {".se",WhoIsProviders.IIS}
            };
        }

        public string GetProvider(string domain)
        {
            int position = domain.LastIndexOf(".", StringComparison.Ordinal);

            if (position == -1 || !whoIsProviders.ContainsKey(domain.Substring(position)))
            {
                throw new InvalidDomainException();
            }
            
            return this.whoIsProviders[domain.Substring(position)];
        }
    }
}