using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class VerisignGrsConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(string whoIsResponseText)
        {
            Dictionary<string, string> whoIsResponse = WhoIsResponseParser.GetWhoIsDictionary(whoIsResponseText);
            
            return new Domain()
            {
                Name = whoIsResponse["Domain Name"].ToLower(),
                DomainId = whoIsResponse["Registry Domain ID"],
                UpdatedDate = DateTime.Parse(whoIsResponse["Updated Date"]),
                CreatedDate = DateTime.Parse(whoIsResponse["Creation Date"]),
                ExpirationDate = DateTime.Parse(whoIsResponse["Registry Expiry Date"]),
                AbuseContactEmail = whoIsResponse["Registrar Abuse Contact Email"],
                AbuseContactPhone = whoIsResponse["Registrar Abuse Contact Phone"],
                NameServers = whoIsResponse["Name Server"],
                RegistarName = whoIsResponse["Registrar"],
                RegistarIanaId = whoIsResponse["Registrar IANA ID"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}