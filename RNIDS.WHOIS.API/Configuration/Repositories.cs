using Microsoft.Extensions.DependencyInjection;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Core.Services;
using RNIDS.WHOIS.TCP;
using RNIDS.WHOIS.TCP.WhoIsInformationConversion;

namespace RNIDS.WHOIS.Configuration
{
    public static class Repositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IWhoIsInformationRepository, WhoIsInformationRepository>();
            services.AddSingleton(new WhoIsProviderRepository());
            services.AddTransient<WhoIsConversionStrategyFactory>();
            
            return services;
        }
    }
}