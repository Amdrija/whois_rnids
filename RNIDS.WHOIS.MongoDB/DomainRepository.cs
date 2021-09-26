using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RNIDS.WHOIS.Application.Interfaces.Repositories;
using RNIDS.WHOIS.Core.Models;
using RNIDS.WHOIS.MongoDB.Options;
using MongoDB.Driver.Linq;

namespace RNIDS.WHOIS.MongoDB
{
    public class DomainRepository : IDomainRepository
    {
        private readonly IMongoCollection<Domain> collection;

        public DomainRepository(
            MongoClient client,
            IOptions<MongoDbOptions> options)
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

        public Task ClearOldAsync(int beforeDays)
        {
            return this.collection.DeleteManyAsync(d =>
                d.SearchedOn < DateTime.Now.AddDays(-beforeDays));
        }

        public Task UpdateAsync(Domain domain)
        {
            return this.collection.ReplaceOneAsync(d => d.Id == domain.Id, domain);
        }

        public Task<List<Domain>> GetPopularAsync()
        {
            return this.collection.AsQueryable<Domain>().OrderByDescending(d => d.SearchCount).Take(5).ToListAsync();
        }

        public Task<Domain> GetRandomAsync()
        {
            return this.collection.AsQueryable<Domain>().Where(d => d.ExpirationDate == null).Sample(1).FirstOrDefaultAsync();
        }
    }
}