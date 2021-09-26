using System;
using System.Collections.Generic;
using System.Globalization;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class RnidsConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(string whoIsResponseText, string domainName)
        {
            if (whoIsResponseText.Contains("ERROR:103"))
            {
                return new Domain()
                {
                    Name = domainName
                };
            }
            
            Dictionary<string, string> whoIsResponse = WhoIsResponseParser.GetWhoIsDictionary(whoIsResponseText);
            
            return new Domain()
            {
                Name = whoIsResponse["Domain name"].ToLower(),
                DomainId = whoIsResponse["ID Number"],
                UpdatedDate = DateTime.ParseExact(whoIsResponse["Modification date"], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.ParseExact(whoIsResponse["Registration date"], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.ParseExact(whoIsResponse["Expiration date"], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Address = whoIsResponse["Address"],
                PostalCode = whoIsResponse["Postal Code"],
                NameServers = whoIsResponse["DNS"],
                RegistarName = whoIsResponse["Registrar"],
                RegistrantName = whoIsResponse["Registrant"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}