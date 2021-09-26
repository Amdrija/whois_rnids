using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RNIDS.WHOIS.Application.Interfaces.Services;
using RNIDS.WHOIS.Smtp.Options;

namespace RNIDS.WHOIS.Smtp
{
    public class SmtpEmailService : IEmailSenderService
    {
            private readonly SmtpClient smtpClient;
    
            private readonly SmtpEmailOptions smtpEmailSettings;
    
            public SmtpEmailService(SmtpClient smtpClient, IOptions<SmtpEmailOptions> smtpEmailSettings)
            {
                this.smtpClient = smtpClient;
                this.smtpEmailSettings = smtpEmailSettings.Value;
            }
    
            public async Task SendEmailAsync(string email, string domain)
            {
                MailAddress from = new MailAddress(this.smtpEmailSettings.Email, this.smtpEmailSettings.DisplayName, Encoding.UTF8);
                MailAddress to = new MailAddress(email);
    
                MailMessage message = new MailMessage(from, to)
                {
                    Body = $"We are reminding you that the domain {domain} is expiring soon.",
                    BodyEncoding = Encoding.UTF8,
    
                    Subject = "Domain Expiration Reminder",
                    SubjectEncoding = Encoding.UTF8,
    
                    IsBodyHtml = true
                };
    
                await this.smtpClient.SendMailAsync(message);
            }
    }
}