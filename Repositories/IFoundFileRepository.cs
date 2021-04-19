using unite.radimaging.source.n2m2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Repositories {
    public interface IFoundFileRepository{
        Task<IEnumerable<FoundFile>> GetFiles();
        Task<FoundFile> GetFile(string id);
        Task<FoundFile> GetFileByPath(string path);
        Task<FoundFile> GetFileByChecksum(string checksum);
        // Buggy (errors in FoundFilerepository.cs) Enable if an when actually needed.
        //Task<IEnumerable<FoundFile>> GetFileByMtime(string mtime);
        //Task<IEnumerable<FoundFile>> GetFileBySize(long size);
        Task CreateFile(FoundFile foundFile);
        Task<bool> UpdateFile(FoundFile foundFile);
        Task<bool> DeleteFile(FoundFile foundFile);
    }
}
