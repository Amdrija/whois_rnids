using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.TCP.Helpers;

namespace RNIDS.WHOIS.TCP.WhoIsInformationConversion
{
    public class TcinetConversionStrategy : IWhoIsInformationConversionStrategy
    {
        public Domain Convert(string whoIsResponseText, string domainName)
        {
            if (whoIsResponseText.Contains("No entries found"))
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
                CreatedDate = DateTime.Parse(whoIsResponse["created"]),
                ExpirationDate = DateTime.Parse(whoIsResponse["paid-till"]),
                NameServers = whoIsResponse["nserver"],
                RegistarName = whoIsResponse["registrar"],
                WhoIsResponse = whoIsResponseText
            };
        }
    }
}