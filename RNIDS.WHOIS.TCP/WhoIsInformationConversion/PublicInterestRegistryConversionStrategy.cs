using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class PublicInterestRegistryConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(Dictionary<string, string> whoIsResponse, string whoIsResponseText)
        {
            return new Domain()
            {
                Name = whoIsResponse["Domain Name"],
                DomainId = whoIsResponse["Registry Domain ID"],
                UpdatedDate = DateTime.Parse(whoIsResponse["Updated Date"]),
                CreatedDate = DateTime.Parse(whoIsResponse["Creation Date"]),
                ExpirationDate = DateTime.Parse(whoIsResponse["Registry Expiry Date"]),
                AbuseContactEmail = whoIsResponse["Registrar Abuse Contact Email"],
                AbuseContactPhone = whoIsResponse["Registrar Abuse Contact Phone"],
                NameServers = whoIsResponse["Name Server"],
                //RegistarName = whoIsResponse["Registrar"],
                RegistarIanaId = whoIsResponse["Registrar IANA ID"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}