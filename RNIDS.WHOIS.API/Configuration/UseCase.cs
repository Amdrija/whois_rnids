using Microsoft.Extensions.DependencyInjection;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.UseCases;

namespace RNIDS.WHOIS.Configuration
{
    public static class UseCase
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<TestUseCase>()
                .AddClasses(classes => classes.Where(type => type.Namespace.StartsWith("RNIDS.WHOIS.Application.UseCases")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }
        
        public static IServiceCollection AddUseCaseLogger(this IServiceCollection services)
        {
            services.TryDecorate(typeof(IUseCase<,>), typeof(LoggingDecorator<,>));
            services.TryDecorate(typeof(IUseCase<>), typeof(LoggingDecoratorNoResponse<>));

            return services;
        }
    }
}