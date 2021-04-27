using unite.radimaging.source.n2m2.Repositories;
using unite.radimaging.source.n2m2.Entities;
using Serilog;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Services {
    public class ProcessFile {
        private readonly IConfiguration _configuration;
        private FoundFileRepository    _repository;
        private ProcessMriFeaturesFile processMriFeaturesFile;
        private ProcessCtFeaturesFile  processCtFeaturesFile;
        private Regex Mripattern;
        private Regex Ctpattern;

        public ProcessFile(IConfiguration configuration, FoundFileRepository repository) {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            processMriFeaturesFile = new ProcessMriFeaturesFile(_configuration, _repository);
            processCtFeaturesFile = new ProcessCtFeaturesFile(_configuration, _repository);
            Mripattern = new Regex(_configuration.GetValue<string>("FileSearchSettings:Mripattern"));
            Ctpattern = new Regex(_configuration.GetValue<string>("FileSearchSettings:Ctpattern"));
        }

        public async Task<Boolean> ProcessNew(FoundFile foundFile) {
            if (Mripattern.IsMatch(foundFile.Path)) return await processMriFeaturesFile.ProcessNew(foundFile); 
            else if (Ctpattern.IsMatch (foundFile.Path)) return await processCtFeaturesFile.ProcessNew(foundFile); 
            else {
                Log.Debug($"File '{foundFile.Path}' is not a valid data file. SKIPPING.");
                return true;
            }
        }

        public async Task<Boolean> ProcessChanged(FoundFile foundFile) {
            if (Mripattern.IsMatch(foundFile.Path)) return await processMriFeaturesFile.ProcessChanged(foundFile);
            else if (Ctpattern.IsMatch(foundFile.Path)) return await processCtFeaturesFile.ProcessChanged(foundFile);
            else {
                Log.Debug($"File '{foundFile.Path}' is not a valid data file. SKIPPING.");
                return true;
            }
        }
    }
}
