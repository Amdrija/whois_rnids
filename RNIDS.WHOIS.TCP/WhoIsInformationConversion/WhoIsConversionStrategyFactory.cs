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
                WhoIsProviders.IIS =>  new IisConversionStrategy(),
                WhoIsProviders.NIC_UK =>  new NicUkConversionStrategy(),
                WhoIsProviders.RNIDS => new RnidsConversionStrategy(),
                WhoIsProviders.MARNET =>  new MarnetConversionStrategy(),
                WhoIsProviders.NIC_KOM =>  new RnidsConversionStrategy(),
                WhoIsProviders.TCINET =>  new TcinetConversionStrategy(),
                WhoIsProviders.VERISIGN_GRS =>  new VerisignGrsConversionStrategy(),
                WhoIsProviders.PUBLIC_INTEREST_REGISTRY =>  new PublicInterestRegistryConversionStrategy(),
                _ => throw new UnsupportedTldException()
            };
        }
    }
}