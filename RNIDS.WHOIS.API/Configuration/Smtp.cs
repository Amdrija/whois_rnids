using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RNIDS.WHOIS.Application.Interfaces.Services;
using RNIDS.WHOIS.Smtp;
using RNIDS.WHOIS.Smtp.Options;

namespace RNIDS.WHOIS.Configuration
{
    public static class Smtp
    {
        public static IServiceCollection AddSmtpEmailClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SmtpEmailOptions>(options => configuration.GetSection("Smtp").Bind(options));
            services.AddTransient<IEmailSenderService, SmtpEmailService>();
            services.AddScoped(serviceProvider =>
            {
                SmtpEmailOptions smtpEmailSettings = serviceProvider.GetRequiredService<IOptions<SmtpEmailOptions>>().Value;

                return new SmtpClient()
                {
                    Host = smtpEmailSettings.Host,
                    Port = smtpEmailSettings.Port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        smtpEmailSettings.Username,
                        configuration.GetSection("Smtp:Password").Value),
                    EnableSsl = true
                };
            });

            return services;
        }
    }
}