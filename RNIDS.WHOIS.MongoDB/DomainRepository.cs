using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.MongoDB.Options;

namespace RNIDS.WHOIS.MongoDB
{
    public class DomainRepository : IDomainRepository
    {
        private readonly IMongoCollection<Domain> collection;

        public DomainRepository(MongoClient client, IOptions<MongoDbOptions> options)
        {
            this.collection = client.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Domain>(options.Value.CollectionName);
        }

        public Task<Domain> GetAsync(string domainName)
        {
            return this.collection.Find<Domain>(d => d.Name == domainName).FirstOrDefaultAsync();
        }

        public Task CreateAsync(Domain domain)
        {
            return this.collection.InsertOneAsync(domain);
        }

        public Task DeleteAsync(Domain domain)
        {
            return this.collection.DeleteOneAsync(d => d.Id == domain.Id);
        }

        public Task UpdateAsync(Domain domain)
        {
            return this.collection.ReplaceOneAsync(d => d.Id == domain.Id, domain);
        }
    }
}