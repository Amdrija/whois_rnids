using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using RNIDS.WHOIS.Application.Base;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Configuration;
using RNIDS.WHOIS.SerilogLogger;

namespace RNIDS.WHOIS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private BackgroundJobServer jobServer;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "RNIDS.WHOIS.API", Version = "v1"});
            });

            services.AddLogger(this.Configuration)
                .AddUseCases()
                .AddUseCaseLogger()
                .AddRepositories()
                .AddMongo(this.Configuration)
                .AddSmtpEmailClient(this.Configuration);

            services.AddHangfire(configuration => configuration
                .UseMongoStorage(connectionString: this.Configuration
                    .GetSection("MongoDb")
                    .GetValue<string>("ConnectionString") + "/hangfire",
                    new MongoStorageOptions()
                    {
                        MigrationOptions = new MongoMigrationOptions()
                        {
                            MigrationStrategy = new MigrateMongoMigrationStrategy(),
                            BackupStrategy = new CollectionMongoBackupStrategy()
                        }
                    }));
            
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient client)
        {
            app.UseHangfireDashboard();
            client.Schedule(() => Console.WriteLine(DateTime.Now), DateTime.Now + TimeSpan.FromSeconds(5));
            client.Enqueue(() => Console.WriteLine("Hello"));
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RNIDS.WHOIS.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}