using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public interface IWhoIsInformationConversionStrategy
    {
        public Domain Convert(string whoIsResponseText, string domainName);
    }
}