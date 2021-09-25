using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RNIDS.WHOIS.MongoDB.Options;

namespace RNIDS.WHOIS.Configuration
{
    public static class MongoDb
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new MongoClient(configuration.GetSection("MongoDb").GetSection("ConnectionString")
                .Value));
            services.Configure<MongoDbOptions>(o => configuration.GetSection("MongoDb").Bind(o));

            return services;
        }
    }
}