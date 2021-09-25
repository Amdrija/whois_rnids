using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Domain.Exception;

namespace RNIDS.WHOIS.Domain.Services
{
    public class WhoIsProviderRepository
    {
        private readonly Dictionary<string, string> whoIsProviders;
        
        public WhoIsProviderRepository()
        {
            this.whoIsProviders = new Dictionary<string, string>
            {
                {".rs", "whois.rnids.rs"},
                {".срб", "whois.rnids.rs"},
                {".ru", "whois.tcinet.ru"},
                {".рф", "whois.tcinet.ru"},
                {".mk", "whois.marnet.mk"},
                {".мкд", "whois.marnet.mk"},
                {".org", "whois.publicinterestregistry.net"},
                {".орг", "whois.publicinterestregistry.net"},
                {".com", "whois.verisign-grs.com"},
                {".ком", "whois.nic.ком"},
                {".net", "whois.verisign-grs.com"},
                {".uk", "whois.nic.uk"},
                {".se ", "whois.iis.se"}
            };
        }

        public string GetProvider(Uri uri)
        {
            string domain = uri.ToString();

            int position = domain.LastIndexOf(".", StringComparison.Ordinal);

            if (position == -1)
            {
                throw new InvalidDomainException();
            }
            
            return this.whoIsProviders[domain.Substring(position)];
        }
    }
}