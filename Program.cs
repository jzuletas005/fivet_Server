﻿
using Fivet.Dao;
using Fivet.ZeroIce.model;
using Fivet.ZeroIce;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;



namespace fivetServer
{   
    /// <summary>
    /// The Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// The Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        /// <summary>
        /// The IHostBuilder
        /// </summary>
        /// <returns></returns>
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
                //The System
                services.AddSingleton<TheSystemDisp_, TheSystemImpl>();
                //Contratos
                services.AddSingleton<ContratosDisp_, ContratosImpl>();
                //The FivetContext
                services.AddDbContext<FivetContext>();
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
