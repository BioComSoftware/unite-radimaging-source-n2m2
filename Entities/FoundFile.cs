using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Entities {
    public class FoundFile {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        //[BsonElement("Path")]
        public string Path{ get; set; }
        public long Size {get; set; }
        public DateTime Mtime { get; set; }
        public string Checksum { get; set;  }

        public bool Equals(FoundFile obj) {
            //Console.WriteLine($"Path: {this.Path},{obj.Path} = {this.Path.Equals(obj.Path)}");
            //Console.WriteLine($"Size: {this.Size},{obj.Size} = {this.Size.Equals(obj.Size)}");
            //Console.WriteLine($"Mtime: {this.Mtime},{obj.Mtime} = {this.Mtime.Equals(obj.Mtime)}");
            //Console.WriteLine($"Checksum: {this.Checksum},{obj.Checksum} = {this.Checksum.Equals(obj.Checksum)}");
            //Check for null and compare run-time types.
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
