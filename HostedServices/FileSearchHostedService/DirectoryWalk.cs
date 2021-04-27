
using Serilog; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace unite.radimaging.source.n2m2.HostedServices.FileSearchHostedService {
    public class DirectoryWalk {
        public static IEnumerable<FileInfo> Walk(string rootPath, Func<FileInfo, bool> Pattern) {
            var directoryStack = new Stack<DirectoryInfo>();
            directoryStack.Push(new DirectoryInfo(rootPath));

            while (directoryStack.Count > 0) {

                var dir = directoryStack.Pop();
                try {
                    foreach (var i in dir.GetDirectories())
                        directoryStack.Push(i);
                }
                catch (UnauthorizedAccessException e) {
                    Log.Warning($"Cant access dir: {e}");
                    continue; // We don't have access to this directory, so skip it
                }
                foreach (var f in dir.GetFiles().Where(Pattern)) // "Pattern" is a function
                    yield return f;
            }
        }
    }
}