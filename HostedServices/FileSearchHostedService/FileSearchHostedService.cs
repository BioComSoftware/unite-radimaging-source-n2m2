using unite.radimaging.source.n2m2.Repositories;
using unite.radimaging.source.n2m2.Entities;
using unite.radimaging.source.n2m2.Data;
using unite.radimaging.source.n2m2.Services;
using Serilog;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace unite.radimaging.source.n2m2.HostedServices.FileSearchHostedService {
    public class FileSearchHostedService : BackgroundService {
        private readonly IConfiguration _configuration;

        public FileSearchHostedService(IConfiguration configuration) {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
            Log.Information("FileSearchHostedService: File searching started.");

            cancellationToken.Register(() => Log.Information("Processing service stopped"));

            FileInfo _current_fileinfo;

            FoundFile _foundFile;
            FoundFile _existingDbFile;

            string dir = _configuration.GetValue<string>("FileSearchSettings:SearchDir");

            FoundFileContext FoundfileContext = new FoundFileContext(_configuration);
            FoundFileRepository _repository = new FoundFileRepository(FoundfileContext);
            ProcessFile processFile = new ProcessFile(_configuration, _repository);

            while (!cancellationToken.IsCancellationRequested) {

                string[] files = Directory.GetFiles(dir);
                foreach (string filename in files) {
                    _current_fileinfo = new FileInfo(filename);
                    _existingDbFile = await _repository.GetFileByPath(filename);
                    _foundFile = new FoundFile(_current_fileinfo);

                    if (_existingDbFile == null) await processFile.ProcessNew(_foundFile);

                    else if (!_foundFile.Equals(_existingDbFile)) await processFile.ProcessChanged(_foundFile);

                    else Log.Debug($"'{filename}' is already processed. No further processing needed.");
                }
                await Task.Delay(_configuration.GetValue<int>("FileSearchSettings:Delay"), cancellationToken);
            }
        }
    }
}
   
