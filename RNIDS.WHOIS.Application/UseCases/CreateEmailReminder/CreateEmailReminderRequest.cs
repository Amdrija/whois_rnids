using System;

namespace RNIDS.WHOIS.Application.UseCases.CreateEmailReminder
{
    public class CreateEmailReminderRequest
    {
        public string Email { get; init; }
        
        public string DomainName { get; init; }
    }
}