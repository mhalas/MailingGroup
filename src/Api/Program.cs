using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;

namespace Api
{
    public class Program
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            Logger.Info("Starting API.");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
