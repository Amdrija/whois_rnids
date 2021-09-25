using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class MarnetConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(Dictionary<string, string> whoIsResponse, string whoIsResponseText)
        {
            return new Domain()
            {
                Name = whoIsResponse["domain"],
                UpdatedDate = DateTime.Parse(whoIsResponse["changed"]),
                CreatedDate = DateTime.Parse(whoIsResponse["registered"]),
                ExpirationDate = DateTime.Parse(whoIsResponse["expire"]),
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