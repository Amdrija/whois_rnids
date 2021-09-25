namespace RNIDS.WHOIS.Domain.Models
{
    public class WhoisInformation
    {
        public  string DomainName { get; init; }
        
        public  bool IsExpired { get; init; }
    }
}