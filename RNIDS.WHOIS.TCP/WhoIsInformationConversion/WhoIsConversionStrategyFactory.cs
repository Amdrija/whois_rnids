using System.Diagnostics;
using RNIDS.WHOIS.Core.Exception;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class WhoIsConversionStrategyFactory
    {
        public IWhoIsInformationConversionStrategy Create(string whoIsProvider)
        {
            return whoIsProvider switch
            {
                WhoIsProviders.IIS =>  new RnidsConversionStrategy(),
                WhoIsProviders.NIC_UK =>  new RnidsConversionStrategy(),
                WhoIsProviders.RNIDS => new RnidsConversionStrategy(),
                WhoIsProviders.MARNET =>  new RnidsConversionStrategy(),
                WhoIsProviders.NIC_KOM =>  new RnidsConversionStrategy(),
                WhoIsProviders.TCINET =>  new TcinetConversionStrategy(),
                WhoIsProviders.VERISIGN_GRS =>  new RnidsConversionStrategy(),
                WhoIsProviders.PUBLIC_INTEREST_REGISTRY =>  new RnidsConversionStrategy(),
                _ => throw new UnsupportedTldException()
            };
        }
    }
}