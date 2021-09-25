using System;
using System.Collections.Generic;
using System.Linq;

namespace RNIDS.WHOIS.TCP.Helpers
{
    public static class WhoIsResponseParser
    {
        public static Dictionary<string, string> GetWhoIsDictionary(string response)
        {
            string parsedResponse = response.Replace(":" + Environment.NewLine, ":");
            
            IEnumerable<string> keyValuePairs = parsedResponse.Split(Environment.NewLine).Where(s => s.Contains(":"));
            return keyValuePairs.Aggregate(new Dictionary<string, string>(), (dictionary, s) =>
            {
                string[] keyValuePair = s.Split(":");
                if (!dictionary.ContainsKey(keyValuePair[0].Trim()))
                {
                    dictionary.Add(keyValuePair[0].Trim(), string.Join(":", keyValuePair[1..]).Trim());
                }

                return dictionary;
            });
        }
    }
}