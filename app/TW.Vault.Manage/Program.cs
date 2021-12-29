using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace TW.Vault.Manage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .ApplyVaultConfiguration()
                .Build();

            Configuration.Require("CaptchaSecretKey", "CaptchaSiteKey");

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseSerilog()
                .UseConfiguration(config)
                .UseStartup<Startup>();
        }
    }
}
