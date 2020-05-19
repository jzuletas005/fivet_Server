using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace fivetServer
{
    class Program
    {

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host
            .CreateDefaultBuilder(args)
            //Development, Staging, Production
            .UseEnvironment("Development")
            //Loggin configuration
            .ConfigureLogging(loggin =>
            {
                loggin.ClearProviders();
                loggin.AddConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.TimestampFormat = "[yyyyMMdd.HHmmss.fff]";
                    options.DisableColors = false;
                });
                loggin.SetMinimumLevel(LogLevel.Trace);
            })
            //Enable Control +C listener
            .UseConsoleLifetime()
            //Service inside the DI
            .ConfigureServices((HostBuilderContext, services)=>
            {
                //The FivetService
                services.AddHostedService<FivetService>();
                /// The logger
                services.AddLogging();
                //the wait for finish
                services.Configure<HostOptions>(option =>
                {
                    option.ShutdownTimeout = System.TimeSpan.FromMinutes(15);
                });
            });
    }
}
