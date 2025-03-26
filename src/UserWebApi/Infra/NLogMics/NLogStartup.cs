using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.PrestashopManager.UserWebApi.Infra.NLogMics
{
    public static class NLogStartup
    {
        public static Logger Init(Assembly assembly)
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(Path.Combine(assemblyFolder, "log")))
            {
                Directory.CreateDirectory(Path.Combine(assemblyFolder, "log"));
            }

            var assemblyShortName = assembly.GetName().Name?.Split('.').Last();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            InternalLogger.LogLevel = NLog.LogLevel.Warn;
            //InternalLogger.LogFile = @$"C:\SatrexStorage\Logs\NlogInternal-{assemblyShortName}.log";
            Logger logger = NLog.LogManager.Setup()
                //.SetupExtensions(s => s.RegisterLayoutRenderer<AspNetAcceptLanguageLayoutRenderer>("aspnet-acceptLanguage"))
                //.RegisterNLogWeb()
                .SetupInternalLogger(p=>p.LogToFile(Path.Combine(assemblyFolder, "log", "nlog-internal.log")))
                .LoadConfigurationFromFile("nlog.config")
                .GetCurrentClassLogger();

            LogManager.Configuration.Variables["AssemblyName"] = assemblyShortName;
            LogManager.Configuration.Variables["InternalLogAddress"] = Path.Combine(assemblyFolder, "log","nlog-internal.log");
            // LogManager.Configuration.Variables["LogDbConnectionString"] = ConfigurationCollector.GetConfigurations<LogDbConfigurations>().ConnectionString;
            LogManager.Configuration.Variables["Environment"] = environment;
            

            logger.Info($"{assemblyShortName} Started In {environment} Environment!");
            return logger;
        }

        public static void LogExitException(this Logger logger, Assembly assembly, Exception exception)
        {
            var assemblyShortName = assembly.GetName().Name?.Split('.').Last();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            logger.Error($"{assemblyShortName} Exited In {environment} Environment Due This Exception: [{exception.Message}]");
        }
    }
}
