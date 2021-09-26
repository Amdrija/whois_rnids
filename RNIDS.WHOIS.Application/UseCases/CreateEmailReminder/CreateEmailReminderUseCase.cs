using System;
using System.Threading.Tasks;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.Core.Services;

namespace RNIDS.WHOIS.Application.UseCases.CreateEmailReminder
{
    public class CreateEmailReminderUseCase : IUseCase<CreateEmailReminderRequest>
    {
        private readonly IEmailReminderRepository emailReminderRepository;
        private readonly IWhoIsInformationRepository whoIsInformationRepository;
        private readonly WhoIsProviderRepository whoIsProviderRepository;

        public CreateEmailReminderUseCase(
            IWhoIsInformationRepository whoIsInformationRepository,
            IEmailReminderRepository emailReminderRepository,
            WhoIsProviderRepository whoIsProviderRepository)
        {
            this.whoIsInformationRepository = whoIsInformationRepository;
            this.emailReminderRepository = emailReminderRepository;
            this.whoIsProviderRepository = whoIsProviderRepository;
        }

        public async Task ExecuteAsync(CreateEmailReminderRequest request)
        {
            Domain domain = await this.whoIsInformationRepository.GetAsync(
                request.DomainName.ToLower(),
                whoIsProviderRepository.GetProvider(request.DomainName));

            //TODO: Remove in production, this is for demonstration puproses
            Domain newDomain = new Domain()
            {
                Name = domain.Name,
                ExpirationDate = DateTime.Now.AddSeconds(15)
            };
            
            this.emailReminderRepository.Create(request.Email, newDomain);
        }
    }
}