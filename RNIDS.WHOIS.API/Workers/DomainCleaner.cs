using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.UseCases.ClearOldDomains;
using RNIDS.WHOIS.Options;

namespace RNIDS.WHOIS.Workers
{
    public class DomainCleaner : BackgroundService
    {
        private Timer timer;
        private readonly IServiceProvider services;
        private readonly ILogger logger;
        private readonly DomainCleanerOptions options;

        public DomainCleaner(IServiceProvider services, ILogger logger, IOptions<DomainCleanerOptions> options)
        {
            this.services = services;
            this.logger = logger;
            this.options = options.Value;
        }

        private async Task DoWork()
        {
            try
            {
                using (IServiceScope scope = this.services.CreateScope())
                {
                    IUseCase<ClearOldDomainsRequest> useCase =
                        scope.ServiceProvider.GetRequiredService<IUseCase<ClearOldDomainsRequest>>();

                    await useCase.ExecuteAsync(new ClearOldDomainsRequest(){BeforeDays = options.CleaningFrequencyInDays});
                }
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int delay = (int) TimeSpan.FromDays(options.CleaningFrequencyInDays).TotalMilliseconds;
                await Task.Delay(delay, stoppingToken);

                await this.DoWork();
            }
        }
    }
}