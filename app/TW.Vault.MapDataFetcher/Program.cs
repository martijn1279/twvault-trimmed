using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace TW.Vault.MapDataFetcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .ApplyVaultConfiguration()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            var host = WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
            
            host.Run();
        }
    }
}
