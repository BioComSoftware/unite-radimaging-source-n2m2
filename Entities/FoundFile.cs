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

        [BsonElement("Path")]
        public string Path{ get; set; }
        public long Size {get; set; }
        public DateTime Mtime { get; set; }
        public string Checksum { get; set;  }
    }
}
