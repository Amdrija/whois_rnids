using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class RnidsConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(Dictionary<string, string> whoIsResponse, string whoIsResponseText)
        {
            return new Domain()
            {
                Name = whoIsResponse["Domain name"],
                DomainId = whoIsResponse["ID Number"],
                UpdatedDate = DateTime.Parse(whoIsResponse["Modification date"]),
                CreatedDate = DateTime.Parse(whoIsResponse["Registration date"]),
                ExpirationDate = DateTime.Parse(whoIsResponse["Expiration date"]),
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