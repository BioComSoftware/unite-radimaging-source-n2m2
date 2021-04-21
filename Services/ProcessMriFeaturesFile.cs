using unite.radimaging.source.n2m2.Repositories;
using unite.radimaging.source.n2m2.Entities;
using Serilog;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Services {
    public class ProcessMriFeaturesFile : SendMriFeatures {
        private readonly IConfiguration _configuration;
        private IFoundFileRepository _repository;
        private string _msg; 

        public ProcessMriFeaturesFile(IConfiguration configuration, FoundFileRepository repository) {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _repository    = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Boolean> ProcessNew(FoundFile foundFile) {

            if (await _repository.CreateFile(foundFile)) Log.Debug($"Created new DB entry for '{foundFile.Path}' (OK).");
            else { 
                Log.Error($"Creating new DB entry for '{foundFile.Path}' (FAILED!). Not continuing to process file. ");
                return false;                 
            }

            return await Process(foundFile);
        }

        public async Task<Boolean> ProcessChanged(FoundFile foundFile) {

            if (await _repository.UpdateFile(foundFile)) Log.Debug($"Updating DB entry for '{foundFile.Path}' (OK).");
            else {
                Log.Error($"Updating DB entry for '{foundFile.Path}' (FAILED!). Not continuing to process file. ");
                return false;
            }

            return await Process(foundFile);
        }

        private async Task<Boolean> Process (FoundFile foundFile) {            

            _msg = $"Processing '{foundFile.Path}' to JSON and sending ...";
            try {
                string _json = MriFeaturesToJson.MriFeaturestoJSON(foundFile.Path);
                if (!await SendMriFeatures.Send(_json)) throw new Exception($"SendMriFeatures.Send returned unsuccessful.");
                Log.Debug(_msg + "OK");
                return true;
            }
            catch (Exception e) {
                Log.Error($"FAILED to process '{foundFile.Path}'. (ERROR: {e}).");
                _msg = $"Removing DB reference for '{foundFile.Path}' ... ";
                if (await _repository.DeleteFile(foundFile)) {
                    Log.Error(_msg + "OK");
                    return false;
                }
                else {
                    Log.Error(_msg + "FAILED! Fatal Error.");
                    throw new Exception("FATAL ERROR accessing DB.");
                } 
            }
        }
    }
}
