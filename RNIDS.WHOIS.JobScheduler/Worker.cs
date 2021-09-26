using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz.Impl;

namespace RNIDS.WHOIS.JobScheduler
{
    public class Worker : BackgroundService
    {
        private readonly StdSchedulerFactory factory;

        public Worker(StdSchedulerFactory factory)
        {
            this.factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this.factory.GetScheduler()
            }
        }
    }
}