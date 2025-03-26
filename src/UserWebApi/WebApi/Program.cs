using System;
using System.Net;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Walrus.PrestashopManager.UserWebApi.Infra.NLogMics;

namespace Walrus.PrestashopManager.UserWebApi.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = NLogStartup.Init(System.Reflection.Assembly.GetExecutingAssembly());

            try
            {
                logger.Debug("init main");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception exception)
            {
                logger.LogExitException(System.Reflection.Assembly.GetExecutingAssembly(), exception);
                throw;
            }
            finally
            {
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
                    webBuilder.ConfigureLogging(options => options.ClearProviders());
                    webBuilder.UseNLog();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
