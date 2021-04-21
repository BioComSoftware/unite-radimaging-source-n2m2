using unite.radimaging.source.n2m2.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace unite.radimaging.source.n2m2.Data {
    public class FoundFileContextseed{
        public static void SeedData(IMongoCollection<FoundFile> fileCollection) {
            bool existCollection = fileCollection.Find(p => true).Any();
            if (!existCollection) {
                FoundFile foundFile = new FoundFile();
                fileCollection.InsertOneAsync(foundFile);
                FilterDefinition<FoundFile> filter = Builders<FoundFile>.Filter.Eq(p => p.Path, foundFile.Path);
                fileCollection.DeleteOne(filter);
            }
        }
    }
}
