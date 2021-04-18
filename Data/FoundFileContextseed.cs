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
                fileCollection.InsertManyAsync(nullFiles());
            }
        }

        private static IEnumerable<FoundFile> nullFiles() {
            return new List<FoundFile>()
            {
                new FoundFile()
                {
                    Path = "/false/fakefile.null",
                    Size = 0,
                    Mtime = DateTime.Now,
                    Checksum ="cc9957363d6e54afb6173379233ca8e0"
                }
            };
        }
    }
}
