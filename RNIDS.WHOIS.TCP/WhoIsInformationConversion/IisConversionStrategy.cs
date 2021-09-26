using System;
using System.Collections.Generic;
using System.Globalization;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class IisConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(string whoIsResponseText)
        {
            Dictionary<string, string> whoIsResponse = WhoIsResponseParser.GetWhoIsDictionary(whoIsResponseText, ":", "\n");
            
            return new Domain()
            {
                Name = whoIsResponse["domain"],
                UpdatedDate = DateTime.ParseExact(whoIsResponse["modified"], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.ParseExact(whoIsResponse["created"], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.ParseExact(whoIsResponse["expires"], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                RegistarName = whoIsResponse["registrar"],
                AbuseContactEmail = whoIsResponse["admin-c"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}