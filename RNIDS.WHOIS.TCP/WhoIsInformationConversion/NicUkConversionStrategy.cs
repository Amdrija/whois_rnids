using System;
using System.Collections.Generic;
using System.Globalization;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class NicUkConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(string whoIsResponseText)
        {
            Dictionary<string, string> whoIsResponse =
                WhoIsResponseParser.GetWhoIsDictionary(whoIsResponseText, ":\r\n", "\r\n\r\n");
            Dictionary<string,string> timeDictionary = WhoIsResponseParser.GetWhoIsDictionary(whoIsResponse["Relevant dates"]);
            return new Domain()
            {
                Name = whoIsResponse["Domain name"],
                UpdatedDate = DateTime.ParseExact(timeDictionary["Last updated"], "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.ParseExact(timeDictionary["Registered on"], "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.ParseExact(timeDictionary["Expiry date"], "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                NameServers = whoIsResponse["Name servers"],
                RegistarName = whoIsResponse["Registrar"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}