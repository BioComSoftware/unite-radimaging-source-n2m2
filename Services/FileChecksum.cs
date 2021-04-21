using System.Security.Cryptography;
using System;
using System.IO;

namespace unite.radimaging.source.n2m2.Services {
    public class FileChecksum {

        public static string getChecksum(string filename) {
            using (var md5 = MD5.Create()) {
                using (var stream = File.OpenRead(filename)) {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
