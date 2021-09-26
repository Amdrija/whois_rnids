using System;
using System.Collections.Generic;
using System.Linq;

namespace RNIDS.WHOIS.TCP.Helpers
{
    public static class WhoIsResponseParser
    {
        public static Dictionary<string, string> GetWhoIsDictionary(string response, string separator = ":", string lineSeparator = "\r\n")
        {
            IEnumerable<string> keyValuePairs = response.Split(lineSeparator).Where(s => s.Contains(separator)).ToList();
            //IEnumerable<string> keyValuePairs = response.Split(separator).ToList();
            return keyValuePairs.Aggregate(new Dictionary<string, string>(), (dictionary, s) =>
            {
                string[] keyValuePair = s.Split(separator);
                if (!dictionary.ContainsKey(keyValuePair[0].Trim()))
                {
                    dictionary.Add(keyValuePair[0].Trim(), string.Join(":", keyValuePair[1..]).Trim());
                }

                return dictionary;
            });
        }
    }
}