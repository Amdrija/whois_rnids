using System.Threading.Tasks;

namespace RNIDS.WHOIS.Application.Interfaces.Services
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync(string emailAddress, string domainName);
    }
}