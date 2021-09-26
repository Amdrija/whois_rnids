using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using RNIDS.WHOIS.Application.Interfaces.Repositories;

namespace RNIDS.WHOIS.Quartz
{
    public class EmailReminderScheduler : IEmailReminderRepository
    {
        private readonly StdSchedulerFactory factory;

        public EmailReminderScheduler(StdSchedulerFactory factory)
        {
            this.factory = factory;
        }

        public async Task CreateAsync(string email, string domainName)
        {
            IScheduler scheduler = await this.factory.GetScheduler();

            await scheduler.Start();
            
            IJobDetail job = JobBuilder.Create<>()
        }
    }
}