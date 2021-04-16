using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using unite.radimaging.source.n2m2.HostedServices;
using Serilog;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2 {
    public class Program {
        public static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Starting CreateHstBuilder...");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging => {
                logging.ClearProviders();
                logging.AddConsole();
                })
            .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices(services => {
                services.AddHostedService<FileSearchHostedService>();
            });
    }
}
