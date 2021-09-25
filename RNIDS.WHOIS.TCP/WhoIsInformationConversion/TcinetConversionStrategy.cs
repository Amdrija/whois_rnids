using System;
using System.Collections.Generic;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class TcinetConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(Dictionary<string, string> whoIsResponse, string whoIsResponseText)
        {
            return new Domain()
            {
                Name = whoIsResponse["domain"],
                CreatedDate = DateTime.Parse(whoIsResponse["created"]),
                ExpirationDate = DateTime.Parse(whoIsResponse["paid-till"]),
                NameServers = whoIsResponse["nserver"],
                RegistarName = whoIsResponse["registrar"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}