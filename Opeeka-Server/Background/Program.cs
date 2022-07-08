using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories;
using Opeeka.PICS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Infrastructure.Caching;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Services;
using System;
using System.IO;
using System.Reflection;

namespace Background
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
                builder.AddUserSecrets<Program>();
            }

            return builder;
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
            var defaultConfiguration = DefaultConfigurationBuilder(args).Build();
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    GetConfiguration(config, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
                    //services.AddHostedService<MyHostedService>();

                    services.AddDbContext<OpeekaDBContext>(options => options.UseSqlServer(configuration.GetValue<string>("OpeekaDatabase")), ServiceLifetime.Scoped);

                    services.AddTransient<IAssessmentResponseRepository, AssessmentResponseRepository>();
                    services.AddTransient<IAssessmentRepository, AssessmentRepository>();
                    services.AddTransient<IPersonQuestionnaireRepository, PersonQuestionnaireRepository>();
                    services.AddTransient<IQuestionnaireRepository, QuestionnaireRepository>();
                    services.AddTransient<IQuestionnaireItemRepository, QuestionnaireItemRepository>();
                    services.AddTransient<IPersonQuestionnaireMetricsRepository, PersonQuestionnaireMetricsRepository>();
                    services.AddTransient<ILookupRepository, LookupRepository>();
                    services.AddTransient<IResponseRepository, ResponseRepository>();
                    services.AddTransient<IQuestionnaireNotifyRiskRuleConditionRepository, QuestionnaireNotifyRiskRuleConditionRepository>();
                    services.AddTransient<INotifiationResolutionStatusRepository, NotifiationResolutionStatusRepository>();
                    services.AddTransient<INotificationTypeRepository, NotificationTypeRepository>();
                    services.AddTransient<INotificationLogRepository, NotificationLogRepository>();
                    services.AddTransient<INotifyRiskRepository, NotifyRiskRepository>();
                    services.AddTransient<IQuestionnaireWindowRepository, QuestionnaireWindowRepository>();
                    services.AddTransient<IAssessmentReasonRepository, AssessmentReasonRepository>();
                    services.AddTransient<IQuestionnaireReminderRuleRespository, QuestionnaireReminderRuleRespository>();
                    services.AddTransient<IQuestionnaireReminderTypeRepository, QuestionnaireReminderTypeRepository>();
                    services.AddTransient<INotifyReminderRepository, NotifyReminderRepository>();
                    services.AddTransient<IPersonQuestionnaireScheduleRepository, PersonQuestionnaireScheduleRepository>();
                    services.AddTransient<IAssessmentStatusRepository, AssessmentStatusRepository>();
                    services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
                    services.AddTransient<IPersonQuestionnaireScheduleService, PersonQuestionnaireScheduleService>();
                    services.AddTransient<IQuestionnaireNotifyRiskRuleRepository, QuestionnaireNotifyRiskRuleRepository>();
                    services.AddTransient<IQuestionnaireNotifyRiskScheduleRepository, QuestionnaireNotifyRiskScheduleRepository>();
                    services.AddTransient<ICollaborationRepository, CollaborationRepository>();
                    services.AddTransient<INotifyRiskValueRepository, NotifyRiskValueRepository>();
                    services.AddTransient<IBackgroundProcessLogRepository, BackgroundProcessLogRepository>();
                    services.AddTransient<IPersonCollaborationRepository, PersonCollaborationRepository>();
                    services.AddTransient<IPersonRepository, PersonRepository>();
                    services.AddTransient<IPersonHelperRepository, PersonHelperRepository>();
                    services.AddTransient<IHelperRepository, HelperRepository>();
                    services.AddTransient<IEmailDetailRepository, EmailDetailRepository>();
                    services.AddTransient<INotificationLevelRepository, NotificationlevelRepository>();

                    services.AddSingleton<ICache, RedisCache>();
                    services.AddAutoMapper(typeof(MappingProfile));

                   
                });
    }
}
