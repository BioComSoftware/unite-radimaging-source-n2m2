using unite.radimaging.source.n2m2.Entities;
using unite.radimaging.source.n2m2.Data;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Data {
    public class FoundFileContext : IFoundFileContext {

        public FoundFileContext(IConfiguration configuration) {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            FoundFiles = database.GetCollection<FoundFile>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            FoundFileContextseed.SeedData(FoundFiles);
            Console.WriteLine("Past SeedData");

        }
        public IMongoCollection<FoundFile> FoundFiles { get; }

    }
}
