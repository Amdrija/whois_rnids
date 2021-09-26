using System;

namespace RNIDS.WHOIS.Application.UseCases.CreateEmailReminder
{
    public class CreateEmailReminderRequest
    {
        public string Email { get; init; }
        
        public Uri DomainName { get; init; }
    }
}