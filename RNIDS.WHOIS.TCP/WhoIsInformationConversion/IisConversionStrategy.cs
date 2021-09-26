using System;
using System.Collections.Generic;
using System.Globalization;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class IisConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(string whoIsResponseText, string domainName)
        {
            if (whoIsResponseText.Contains("not found"))
            {
                return new Domain()
                {
                    Name = domainName
                };
            }
            
            Dictionary<string, string> whoIsResponse = WhoIsResponseParser.GetWhoIsDictionary(whoIsResponseText, ":", "\n");
            
            return new Domain()
            {
                Name = whoIsResponse["domain"].ToLower(),
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