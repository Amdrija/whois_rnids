using System;
using System.Collections.Generic;
using System.Globalization;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class MarnetConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(string whoIsResponseText, string domainName)
        {
            if (whoIsResponseText.Contains("ERROR:101"))
            {
                return new Domain()
                {
                    Name = domainName
                };
            }
            
            Dictionary<string, string> whoIsResponse = WhoIsResponseParser.GetWhoIsDictionary(whoIsResponseText);
            
            return new Domain()
            {
                Name = whoIsResponse["domain"].ToLower(),
                UpdatedDate = DateTime.ParseExact(whoIsResponse["changed"], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.ParseExact(whoIsResponse["registered"], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.ParseExact(whoIsResponse["expire"], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                NameServers = whoIsResponse["nserver"],
                Address = whoIsResponse["address"],
                RegistarName = whoIsResponse["registrar"],
                AbuseContactEmail = whoIsResponse["e-mail"],
                AbuseContactPhone = whoIsResponse["phone"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}