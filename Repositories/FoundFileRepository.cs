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

        // Buggy. enable these if and when needed. 
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
            // A proper update is failing for an unknown reason (no error. Just
            //  not succeeding and returning false
            // Use delete and create for now
            /*var updateResult = await _context.FoundFiles.ReplaceOneAsync(
                filter: g => g.Id == foundFile.Id, replacement: foundFile);
            
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;*/
            Boolean _result;
            // Get the original file data first. If it fails, update should never have been called. 
            FoundFile _originalFoundFile = await this.GetFileByPath(foundFile.Path);
            if (_originalFoundFile == null) { throw new ApplicationException("FoundFileRepository.UpdateFile could not obtain original document prior to updating. Halting. Are you shure you needed update and not CreateFile?");  }

            _result = await this.DeleteFile(foundFile);
            if (!_result) { throw new ApplicationException("FoundFileRepository.UpdateFile could not successfully delete the document prior to updating. Halting."); }
            
            await this.CreateFile(foundFile);
            return true; 
        }

        public async Task<bool> DeleteFile(FoundFile foundfile) {
            string path = foundfile.Path; 
            FilterDefinition<FoundFile> filter = Builders<FoundFile>.Filter.Eq(p => p.Path, path);

            DeleteResult deleteResult = await _context
                                                .FoundFiles
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
