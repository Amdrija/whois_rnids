using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.SerilogLogger;

namespace RNIDS.WHOIS.Configuration
{
    public static class LoggerConfiguration
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILogger, SerilogLogger.SerilogLogger>();
            services.Configure<SentryOptions>(o => configuration.GetSection("Sentry").Bind(o));

            return services;
        }
    }
}