using Microsoft.Extensions.DependencyInjection;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Domain.Services;
using RNIDS.WHOIS.TCP;

namespace RNIDS.WHOIS.Configuration
{
    public static class Repositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IWhoIsInformationRepository, WhoIsInformationRepository>();
            services.AddSingleton(new WhoIsProviderRepository());
            
            return services;
        }
    }
}