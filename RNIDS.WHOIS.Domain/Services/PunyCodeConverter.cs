using System;
using System.Globalization;

namespace RNIDS.WHOIS.Domain.Services
{
    public static class PunyCodeConverter
    {
        public static string GetPuny(this string @string)
        {
            IdnMapping mapping = new IdnMapping();
            string ascii = mapping.GetAscii(@string);
            return ascii;
        }
    }
}