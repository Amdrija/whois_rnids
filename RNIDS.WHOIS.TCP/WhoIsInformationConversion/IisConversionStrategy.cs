using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class IisConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(Dictionary<string, string> whoIsResponse, string whoIsResponseText)
        {
            return new Domain()
            {
                Name = whoIsResponse["domain"],
                UpdatedDate = DateTime.Parse(whoIsResponse["modified"]),
                CreatedDate = DateTime.Parse(whoIsResponse["created"]),
                ExpirationDate = DateTime.Parse(whoIsResponse["expires"]),
                RegistarName = whoIsResponse["registrar"],
                AbuseContactEmail = whoIsResponse["admin-c"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}