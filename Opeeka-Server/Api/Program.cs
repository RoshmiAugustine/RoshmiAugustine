// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Api.Config;
    using Opeeka.PICS.Infrastructure.Data;
    using Serilog;

    public class Program
    {
        private IConfiguration Config { get; }

        public static string WorkingDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static IConfiguration Configuration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            return new ConfigurationBuilder()
                .SetBasePath(WorkingDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static IConfigurationBuilder DefaultConfigurationBuilder(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args);
            if (environment == "Development")
            {
                builder.AddUserSecrets<Startup>();
            }

            return builder;
        }

        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            SerilogConfig.Configure(serviceCollection, GetConfiguration(DefaultConfigurationBuilder(args), args));
            Log.Verbose("Starting web host");
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var opeekaDbContext = services.GetRequiredService<OpeekaDBContext>();
                opeekaDbContext.Database.EnsureCreated();

                //await GeneralContextSeed.SeedAsync(opeekaDbContext, loggerFactory);
            }

            host.Run();
        }

        /// <summary>
        /// GetConfiguration.
        /// </summary>
        /// <param name="configurationBuilder">configurationBuilder.</param>
        /// <param name="args">args.</param>
        /// <returns>IConfiguration.</returns>
        public static IConfiguration GetConfiguration(IConfigurationBuilder configurationBuilder, string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var defaultConfiguration = configurationBuilder.Build();
            if (environment == "Development")
            {
                return configurationBuilder.Build();
            }
            else
            {
                var config = configurationBuilder.AddAzureKeyVault(
                    defaultConfiguration.GetValue<string>("KeyVault:Url"),
                    defaultConfiguration.GetValue<string>("KeyVault:ClientId"),
                    defaultConfiguration.GetValue<string>("KeyVault:SecretId")
                );
                return config.Build();
            }
        }

        /// <summary>
        /// CreateHostBuilder.
        /// </summary>
        /// <param name="args">args.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSerilog()
            .ConfigureAppConfiguration((context, config) => { })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseConfiguration(GetConfiguration(DefaultConfigurationBuilder(args), args));
                webBuilder.UseIISIntegration();
                webBuilder.UseStartup<Startup>();
            });
    }
}
