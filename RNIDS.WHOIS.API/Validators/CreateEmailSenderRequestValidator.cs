using System;
using FluentValidation;
using RNIDS.WHOIS.Application.UseCases.CreateEmailReminder;
using RNIDS.WHOIS.Application.UseCases.GetWhoIsInformation;
using RNIDS.WHOIS.Core.Services;

namespace RNIDS.WHOIS.Validators
{
    public class CreateEmailSenderRequestValidator : AbstractValidator<CreateEmailReminderRequest>
    {
        public CreateEmailSenderRequestValidator()
        {
            this.RuleFor(r => r.DomainName.ToLower().GetPuny()).Matches(GetWhoIsInformationRequestValidator.DOMAIN_REGEX).WithMessage("Invalid domain name.");
            this.RuleFor(r => r.Email).NotEmpty().EmailAddress();
        }
    }
}