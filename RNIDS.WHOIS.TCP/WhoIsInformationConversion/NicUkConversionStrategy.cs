using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class NicUkConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(Dictionary<string, string> whoIsResponse, string whoIsResponseText)
        {
            Dictionary<string,string> helpDictionary = WhoIsResponseParser.GetWhoIsDictionary(whoIsResponse["Relevant dates"]);
            return new Domain()
            {
                Name = whoIsResponse["Domain name"],
                UpdatedDate = DateTime.Parse(whoIsResponse["Last updated"]),
                CreatedDate = DateTime.Parse(helpDictionary["Registered on"]),
                ExpirationDate = DateTime.Parse(whoIsResponse["Expiry date"]),
                NameServers = whoIsResponse["Name servers"],
                RegistarName = whoIsResponse["Registrar"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}