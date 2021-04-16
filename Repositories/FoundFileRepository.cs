 using unite.radimaging.source.n2m2.Data;
using unite.radimaging.source.n2m2.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Repositories {
    public class FoundFileRepository : IFoundFileRepository {
        private readonly IFoundFileContext _context;
        public FoundFileRepository(IFoundFileContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<FoundFile>> GetFiles() {
            return await _context
                .FoundFiles
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<FoundFile> GetFile(string id) {
            return await _context
                           .FoundFiles
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<FoundFile> GetFileByPath(string path) {
            return await _context
                           .FoundFiles
                           .Find(p => p.Path == path)
                           .FirstOrDefaultAsync();
        }

        public async Task<FoundFile> GetFileByChecksum(string checksum) {
            return await _context
                           .FoundFiles
                           .Find(p => p.Checksum == checksum)
                           .FirstOrDefaultAsync();
        }

        //public async Task<IEnumerable<FoundFile>> GetFileByMtime(string mtime) {
        //    FilterDefinition<FoundFile> filter = Builders<FoundFile>.Filter.ElemMatch<DateTime>(p => p.Mtime, mtime);

        //    return await _context
        //                    .FoundFiles
        //                    .Find(filter)
        //                    .ToListAsync();
        //}

        //public async Task<IEnumerable<FoundFile>> GetFileBySize(long size) {
        //    FilterDefinition<FoundFile> filter = Builders<FoundFile>.Filter.ElemMatch<long>(p => p.Size, size);

        //    return await _context
        //                    .FoundFiles
        //                    .Find(filter)
        //                    .ToListAsync();
        //}

        public async Task CreateFile(FoundFile foundFile) {
            await _context.FoundFiles.InsertOneAsync(foundFile);
        }

        public async Task<bool> UpdateFile(FoundFile foundFile) {
            var updateResult = await _context.FoundFiles.ReplaceOneAsync(
                filter: g => g.Path == foundFile.Path, replacement: foundFile);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteFile(string path) {
            FilterDefinition<FoundFile> filter = Builders<FoundFile>.Filter.Eq(p => p.Path, path);

            DeleteResult deleteResult = await _context
                                                .FoundFiles
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
