using System;

namespace RNIDS.WHOIS.Core.Models
{
    public class Domain
    {
        public Guid Id { get; init; }
        
        public string DomainId { get; init; }
        
        public string Name { get; init; }
        
        public DateTime? CreatedDate { get; init; }
        
        public DateTime? UpdatedDate { get; init; }
        
        public DateTime? ExpirationDate { get; init; }
        
        public string NameServers { get; init; }
        
        public string Address { get; init; }
        
        public string PostalCode { get; init; }
        
        public string RegistarIanaId { get; init; }
        
        public string RegistarName { get; init; }
        
        public string Url { get; init; }
        
        public string AbuseContactEmail { get; init; }
        
        public string AbuseContactPhone { get; init; }
        
        public string RegistrantName { get; init; }
        
        public string WhoIsResponse { get; init; }
        
        public int SearchCount { get; private set; }
        
        public DateTime SearchedOn { get; private set; }

        private const int SOON_EXPIRING_DAYS = 1;

        public Domain()
        {
            this.Id = Guid.NewGuid();
            this.SearchCount = 1;
            this.SearchedOn = DateTime.Now;
        }

        public void IncrementSearch()
        {
            this.SearchCount++;
            this.SearchedOn = DateTime.Now;
        }

        public bool IsExpiringSoon()
        {
            return this.ExpirationDate == null || this.ExpirationDate?.Subtract(DateTime.Now).TotalDays <= 1;
        }
    }
}