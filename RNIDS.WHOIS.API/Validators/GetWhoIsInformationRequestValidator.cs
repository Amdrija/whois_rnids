using FluentValidation;
using RNIDS.WHOIS.Application.UseCases.GetWhoIsInformation;
using RNIDS.WHOIS.Core.Services;

namespace RNIDS.WHOIS.Validators
{
    public class GetWhoIsInformationRequestValidator : AbstractValidator<GetWhoIsInformationRequest>
    {
        public const string DOMAIN_REGEX = @"^[\w\d-]+\.[\w\d-]+(\.[\w\d-]+)?$";
        
        public GetWhoIsInformationRequestValidator()
        {
            this.RuleFor(r => r.Domain.ToLower().GetPuny()).Matches(DOMAIN_REGEX).WithMessage("Invalid domain name.");
        }
    }
}