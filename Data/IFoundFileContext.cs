using unite.radimaging.source.n2m2.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Data {
    public interface IFoundFileContext {
        IMongoCollection<FoundFile> FoundFiles { get; }
    }
}
