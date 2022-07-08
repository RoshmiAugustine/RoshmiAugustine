// -----------------------------------------------------------------------
// <copyright file="SerilogConfig.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Configuration;

namespace Opeeka.PICS.Infrastructure.Logging
{
    /// <summary>
    /// To manage and configure serilog sinks, enrichers and loglevel.
    /// </summary>
    public class SerilogConfig
    {
        /// <summary>
        /// Configures the serilog sinks, enrichers and loglevel.
        /// </summary>
        /// <param name="serviceCollection">The instance of <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">The configuration properties.</param>
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var baseLogPath = configuration["Logging:Path"];
            if (string.IsNullOrEmpty(baseLogPath))
            {
                throw new ConfigurationErrorsException("Logging:Path is not set in the app.settings.");
            }

            var deploymentMode = configuration["DeploymentMode"];
            if (string.IsNullOrEmpty(deploymentMode))
            {
                throw new ConfigurationErrorsException("DeploymentMode is not set in the app.settings.");
            }

            var appName = configuration["ApplicationName"];
            if (string.IsNullOrEmpty(appName))
            {
                throw new ConfigurationErrorsException("ApplicationName is not set in the app.settings.");
            }

            var workspaceId = configuration["AzureAnalytics-workspaceId"];
            if (string.IsNullOrWhiteSpace(workspaceId))
            {
                throw new ConfigurationErrorsException("workspaceId is not set in the app.settings.");
            }

            var authenticationId = configuration["AzureAnalytics-authenticationId"];
            if (string.IsNullOrWhiteSpace(authenticationId))
            {
                throw new ConfigurationErrorsException("authenticationId is not set in the app.settings.");
            }

            var appLogName = $"{appName}-{deploymentMode.ToLower()}";
            var AzureAnalyticsFile = baseLogPath + "\\" + appLogName + $"-{DateTime.Now.ToString("yyyy-MM-dd")}.log";

            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] <" + deploymentMode + "|{SourceContext}|{CorrelationId}> {Message}{NewLine}{NewLine}{Exception}{NewLine}";

            switch (deploymentMode.ToUpper())
            {
                case "LOCAL":
                    // This will only write to the local file system as this is the dev's local machine.
                    SerilogLoggingLevelSwitch.SetLoggingLevel((int)LogEventLevel.Debug);
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithEnvironmentUserName()
                        .MinimumLevel.ControlledBy(SerilogLoggingLevelSwitch.LevelSwitch)
                        .WriteTo.File(AzureAnalyticsFile, SerilogLoggingLevelSwitch.LevelSwitch.MinimumLevel, outputTemplate, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 1024 * 1024 * 100) // 100MB
                        .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                        .CreateLogger();
                    break;

                case "DEV":
                case "QA":
                    SerilogLoggingLevelSwitch.SetLoggingLevel((int)LogEventLevel.Warning);
                    Log.Logger = new LoggerConfiguration()
                    .WriteTo.AzureAnalytics(workspaceId, authenticationId, "Log", LogEventLevel.Warning)
                    .CreateLogger();
                    break;

                case "STAGE":
                case "PRODUCTION":
                case "PRODUCTION1":
                case "DEMO":
                case "TRAINING":
                    SerilogLoggingLevelSwitch.SetLoggingLevel((int)LogEventLevel.Warning);
                    Log.Logger = new LoggerConfiguration()
                     .WriteTo.AzureAnalytics(workspaceId, authenticationId, "Log", LogEventLevel.Warning)
                     .CreateLogger();
                    break;

                default:
                    SerilogLoggingLevelSwitch.SetLoggingLevel((int)LogEventLevel.Warning);
                    Log.Logger = new LoggerConfiguration()
                     .WriteTo.AzureAnalytics(workspaceId, authenticationId, "Log", LogEventLevel.Warning)
                     .CreateLogger();
                    break;
            }

            serviceCollection.AddLogging(lb => lb.AddSerilog(dispose: true));

            var logger = Log.ForContext<SerilogConfig>();
            if (deploymentMode.ToUpper() == "LOCAL")
            {
                logger.Information($"Detailed log file will be written to {AzureAnalyticsFile}");
            }
            else
            {
                logger.Information($"Detailed log data is being sent to Datadog under the service name {appName}.");
            }

            //logger.Debug("Startup -> Logging Configuration: COMPLETE");
        }
    }
}
