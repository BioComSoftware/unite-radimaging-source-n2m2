using Serilog;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Services {
    public class SendMriFeatures {

        public static async Task<Boolean> Send(string jsondata) {

            var _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            string HttpBaseAddressUri = _configuration.GetValue<string>("RestSettings:HttpBaseAddressUri");
            string MriFeaturesApi = _configuration.GetValue<string>("RestSettings:MriFeaturesApi");

            using (var client = new HttpClient()) {

                client.BaseAddress = new Uri(HttpBaseAddressUri);
                
                var response = await client.PostAsJsonAsync(MriFeaturesApi, jsondata);

                if (response.IsSuccessStatusCode == true)  return true;
                else return false;
            }
        }
    }
}
