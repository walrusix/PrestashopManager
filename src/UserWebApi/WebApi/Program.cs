using System;
using System.Net;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApi;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using Sentry;
using Walrus.PrestashopManager.UserWebApi.Infra.NLogMics;

namespace Walrus.PrestashopManager.UserWebApi.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //WebRequest.DefaultWebProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };
            //var logger = LogManager.GetCurrentClassLogger();
            var logger = NLogStartup.Init(System.Reflection.Assembly.GetExecutingAssembly());

            try
            {
                logger.Debug("init main");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.LogExitException(System.Reflection.Assembly.GetExecutingAssembly(), exception);
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Flush();
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(options => options.ClearProviders())
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureLogging(options => options.ClearProviders());
                    webBuilder.UseNLog();
                    webBuilder.UseStartup<Startup>();
                });

        //private static void UsingCodeConfiguration()
        //{
        //    // Other overloads exist, for example, configure the SDK with only the DSN or no parameters at all.
        //    var config = new LoggingConfiguration();

        //    config.AddSentry(options =>
        //    {
        //        options.Layout = "${message}";
        //        options.BreadcrumbLayout = "${logger}: ${message}"; // Optionally specify a separate format for breadcrumbs

        //        options.MinimumBreadcrumbLevel = NLog.LogLevel.Debug; // Debug and higher are stored as breadcrumbs (default is Info)
        //        options.MinimumEventLevel = NLog.LogLevel.Error; // Error and higher is sent as event (default is Error)

        //        // If DSN is not set, the SDK will look for an environment variable called SENTRY_DSN. If
        //        // nothing is found, SDK is disabled.
        //        options.Dsn = "https://a48f67497c814561aca2c66fa5ee37fc:a5af1a051d6f4f09bdd82472d5c2629d@sentry.io/1340240";

        //        options.AttachStacktrace = true;
        //        options.SendDefaultPii = true; // Send Personal Identifiable information like the username of the user logged in to the device

        //        options.IncludeEventDataOnBreadcrumbs = true; // Optionally include event properties with breadcrumbs
        //        options.ShutdownTimeoutSeconds = 5;

        //        options.AddTag("logger", "${logger}");  // Send the logger name as a tag

        //        options.HttpProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };
        //        // Other configuration
        //    });

        //    config.AddTarget(new DebuggerTarget("Debugger"));
        //    config.AddTarget(new ColoredConsoleTarget("Console"));

        //    config.AddRuleForAllLevels("Console");
        //    config.AddRuleForAllLevels("Debugger");

        //    LogManager.Configuration = config;
        //}
    }
}
