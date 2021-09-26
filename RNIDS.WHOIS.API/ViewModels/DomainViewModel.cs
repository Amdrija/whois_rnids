using System;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.ViewModels
{
    public class DomainViewModel
    {
        public Guid Id { get; init; }
        
        public string DomainId { get; init; }
        
        public string Name { get; init; }
        
        public double CreatedDateInMiliseconds { get; init; }
        
        public double? UpdatedDateInMiliseconds { get; init; }
        
        public double? ExpirationDateInMiliseconds { get; init; }
        
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

        public DomainViewModel(Domain domain)
        {
            DateTime utc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            this.Id = domain.Id;
            this.DomainId = domain.DomainId;
            this.Name = domain.Name;
            this.CreatedDateInMiliseconds = domain.CreatedDate.ToUniversalTime().Subtract(utc)
                .TotalMilliseconds;
            this.UpdatedDateInMiliseconds = domain.UpdatedDate?.ToUniversalTime().Subtract(utc)
                .TotalMilliseconds;
            this.ExpirationDateInMiliseconds = domain.ExpirationDate?.ToUniversalTime().Subtract(utc)
                .TotalMilliseconds;
            this.NameServers = domain.NameServers;
            this.Address = domain.Address;
            this.PostalCode = domain.PostalCode;
            this.RegistarIanaId = domain.RegistarIanaId;
            this.RegistarName = domain.RegistarName;
            this.Url = domain.Url;
            this.AbuseContactEmail = domain.AbuseContactEmail;
            this.AbuseContactPhone = domain.AbuseContactPhone;
            this.RegistrantName = domain.RegistrantName;
            this.WhoIsResponse = domain.WhoIsResponse;
        }
    }
}