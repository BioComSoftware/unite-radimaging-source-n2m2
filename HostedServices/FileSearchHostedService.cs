using unite.radimaging.source.n2m2.Repositories;
using unite.radimaging.source.n2m2.Entities;
using unite.radimaging.source.n2m2.Data;
using Serilog;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace unite.radimaging.source.n2m2.HostedServices {
    public class FileSearchHostedService : BackgroundService {
        private readonly IConfiguration _configuration;
        //private IFoundFileRepository _repository;

        public FileSearchHostedService(
            //IFoundFileRepository repository,
            IConfiguration configuration
            ) {
            //_repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
            Log.Information("FileSearchHostedService: File searching started.");

            cancellationToken.Register(() => Log.Information("Processing service stopped"));

            //string checksum;
            string _current_path;
            FileInfo _current_file;
            FoundFile _foundFile;
            FoundFile _existingFile;
            FoundFile _addFoundFile;
            FoundFileContext FoundfileContext = new FoundFileContext(_configuration); // Normal instantiation, in leiu of injection
            FoundFileRepository _repository = new FoundFileRepository(FoundfileContext);  // Normal instantiation, in leiu of injection

            while (!cancellationToken.IsCancellationRequested) {
                Log.Information("Searching files...");
                string dir = _configuration.GetValue<string>("FileSearchSettings:SearchDir");
                string[] files = Directory.GetFiles(dir);
                foreach (string filename in files) {
                    Log.Debug($"Parsing '{filename}'");

                    _current_file     = new FileInfo(filename);
                    _current_path = Path.GetFullPath(Path.Combine(_current_file.Directory.ToString(), _current_file.Name));

                    _foundFile = new FoundFile() {
                        Path = _current_path,
                        Size = _current_file.Length,
                        Mtime = _current_file.LastWriteTime,
                        Checksum = FileChecksum.getChecksum(filename)
                    };
                    Console.WriteLine("==========================");
                    _existingFile = await _repository.GetFileByPath (_current_path);
                    Console.WriteLine("past _repository.GetFileByPath");

                    try {
                        Console.WriteLine($"{_existingFile.Path} exists!!!");
                        Console.WriteLine($"     _existingFile.Checksum: {_existingFile.Checksum}");
                        Console.WriteLine($"     _existingFile.Length = {_existingFile.Size}");
                        Console.WriteLine($"     _existingFile.LastWriteTime = ({_existingFile.Mtime}");
                    }
                    catch {
                        Console.WriteLine($"Creating MongoDB for file {_foundFile.Path} ");
                        _addFoundFile = new FoundFile() {
                            Path = _foundFile.Path,
                            Size = _foundFile.Size,
                            Mtime = _foundFile.Mtime,
                            Checksum = _foundFile.Checksum
                        };
                        await _repository.CreateFile(_addFoundFile);
                        //Console.WriteLine($"_current_file: {_foundFile.Path}");
                        //Console.WriteLine($"checksum: {_foundFile.Checksum}");
                        //Console.WriteLine($"_current_file.Length = {_foundFile.Size}");
                        //Console.WriteLine($"_current_file.LastWriteTime = ({_foundFile.Mtime}");
                        //Console.WriteLine($"_current_file.Attributes = {_current_file.Attributes}");
                    }
                }

                await Task.Delay(
                    _configuration.GetValue<int>("FileSearchSettings:Delay"),
                    cancellationToken
                    );
            }
        }
    }
}
