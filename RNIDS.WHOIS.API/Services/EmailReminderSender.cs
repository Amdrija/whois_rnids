using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.Smtp.Options;

namespace RNIDS.WHOIS.Services
{
    public class EmailReminderSender : IEmailReminderRepository
    {
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly SmtpEmailOptions options;
        private readonly IConfiguration configuration;

        public EmailReminderSender(
            IBackgroundJobClient backgroundJobClient,
            IOptions<SmtpEmailOptions> options,
            IConfiguration configuration)
        {
            this.backgroundJobClient = backgroundJobClient;
            this.configuration = configuration;
            this.options = options.Value;
        }

        public void Create(string email, Domain domain)
        {
            if (domain.ExpirationDate != null)
            {
                this.backgroundJobClient.Schedule(() =>
                        EmailReminderSender.SendEmailAsync(
                            email,
                            domain.Name,
                            this.options,
                            this.configuration.GetSection("Smtp:Password").Value),
                    new DateTimeOffset((DateTime) domain.ExpirationDate));
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static async Task SendEmailAsync(
            string email,
            string domainName,
            SmtpEmailOptions options,
            string password)
        {
            MailAddress from = new MailAddress(options.Email, options.DisplayName, Encoding.UTF8);
            MailAddress to = new MailAddress(email);
    
            MailMessage message = new MailMessage(from, to)
            {
                Body = $"We are reminding you that the domain {domainName} is expiring soon.",
                BodyEncoding = Encoding.UTF8,
    
                Subject = "Domain Expiration Reminder",
                SubjectEncoding = Encoding.UTF8,
    
                IsBodyHtml = true
            };
            
            Console.WriteLine("Sending Email");

            SmtpClient client = new SmtpClient()
            {
                Host = options.Host,
                Port = options.Port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    options.Email,
                    password),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
            
            Console.WriteLine("Email sent");
        }
    }
}