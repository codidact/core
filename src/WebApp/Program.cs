using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Codidact.Core.WebApp
{
    public class Program
    {
        // Task Main makes an instance of
        // HostBUilder and runs it asynchronously
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        // Create host builder creates a host builder for the website.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
