using unite.radimaging.source.n2m2.Services; 
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.IO;

namespace unite.radimaging.source.n2m2.Entities {
    public class FoundFile {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Path{ get; set; }
        public long Size {get; set; }
        public DateTime Mtime { get; set; }
        public string Checksum { get; set;  }

        public FoundFile(string filename) {
            FileInfo fileinfo = new FileInfo(filename);
            SetVals(fileinfo);
        }

        public FoundFile() {
            // Can add a default FoundFile to config, but why? :D
            Guid _guid = Guid.NewGuid();
            Path = $"/fakefile/{_guid}.null";
            Size = 0;
            Mtime = DateTime.Now;
            Checksum = _guid.ToString();
        }

        public FoundFile(FileInfo fileinfo) {
            SetVals(fileinfo);
        }

        public FoundFile(string path, long size, DateTime mtime) {
            Path = path;
            Size = size;
            Mtime = mtime;
            Checksum = FileChecksum.getChecksum(path);
        }

        public FoundFile(string path, long size, DateTime mtime, string checksum) {
            Path = path;
            Size = size;
            Mtime = mtime;
            Checksum = checksum;
        }

        private void SetVals (FileInfo fileinfo) { 
            Path = fileinfo.FullName;
            Size = fileinfo.Length;
            Mtime = fileinfo.LastWriteTime;
            Checksum = FileChecksum.getChecksum(fileinfo.FullName);
        }

        public bool Equals(FoundFile obj) {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
                return false;
            }

            // Only the path and the checksum are relaly important. The rest is just for reference.
            else if (this.Path == obj.Path && this.Checksum == obj.Checksum) {
                return true;
            }

            else {
                return false;
            }
        }
    }
}
