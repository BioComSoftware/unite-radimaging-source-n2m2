using unite.radimaging.source.n2m2.Entities;
using MongoDB.Driver;

namespace unite.radimaging.source.n2m2.Data {
    public interface IFoundFileContext {
        IMongoCollection<FoundFile> FoundFiles { get; }
    }
}
