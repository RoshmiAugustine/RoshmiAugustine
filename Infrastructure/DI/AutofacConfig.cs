// -----------------------------------------------------------------------
// <copyright file="AutofacConfig.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Infrastructure.DI
{
    using Autofac;
    using Opeeka.PICS.Domain.Interfaces;
    using Opeeka.PICS.Domain.Interfaces.Common;
    using Opeeka.PICS.Domain.Interfaces.Repositories;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Domain.Services;
    using Opeeka.PICS.Infrastructure.Common;
    using Opeeka.PICS.Infrastructure.Data.Repositories;
    using Opeeka.PICS.Infrastructure.Infrastructure.Mapper;
    using Opeeka.PICS.Domain.Identity;
    using Microsoft.AspNetCore.Identity;
    using Opeeka.PICS.Domain.Entities;
    using Opeeka.PICS.Domain.Interfaces.Providers.Contract;
    using Opeeka.PICS.Domain.Providers.Implementations;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Defines the <see cref="AutofacConfig" />.
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// here we are registering the Autofac Configuration like Dependency injection,Automapper configuration.
        /// </summary>
        /// <param name="builder">Container builder parameter.</param>
        public static void Register(ContainerBuilder builder)
        {

            MapperConfig.Initialize(builder);

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            // registering Services
            builder.RegisterType<AgencyService>().As<IAgencyService>().InstancePerLifetimeScope();
            builder.RegisterType<LookupService>().As<ILookupService>().InstancePerLifetimeScope();
            // builder.RegisterType<WeatherForecastService>().As<IWeatherForecastService>().InstancePerLifetimeScope();
            builder.RegisterType<HelperService>().As<IHelperService>().InstancePerLifetimeScope();
            builder.RegisterType<InsightsService>().As<IInsightsService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<JWTTokenService>().As<IJWTTokenService>().InstancePerLifetimeScope();
            builder.RegisterType<PersonService>().As<IPersonService>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireService>().As<IQuestionnaireService>().InstancePerLifetimeScope();
            builder.RegisterType<CollaborationService>().As<ICollaborationService>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationObjectTypeService>().As<IApplicationObjectTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<ReportingUnitService>().As<IReportingUnitService>().InstancePerLifetimeScope();
            builder.RegisterType<AssessmentService>().As<IAssessmentService>().InstancePerLifetimeScope();
            builder.RegisterType<OptionsService>().As<IOptionsService>().InstancePerLifetimeScope();
            builder.RegisterType<SystemRoleService>().As<ISystemRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationLogService>().As<INotificationLogService>().InstancePerLifetimeScope();
            builder.RegisterType<PersonQuestionnaireMetricsService>().As<IPersonQuestionnaireMetricsService>().InstancePerLifetimeScope();
            builder.RegisterType<SearchService>().As<ISearchService>().InstancePerLifetimeScope();
            builder.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();
            builder.RegisterType<FileService>().As<IFileService>().InstancePerLifetimeScope();
            builder.RegisterType<UserProfileService>().As<IUserProfileService>().InstancePerLifetimeScope();
            builder.RegisterType<PersonQuestionnaireScheduleService>().As<IPersonQuestionnaireScheduleService>().InstancePerLifetimeScope();
            builder.RegisterType<ExportImportService>().As<IExportImportService>().InstancePerLifetimeScope();

            // registering Repositories
            builder.RegisterType<AgencyRepository>().As<IAgencyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AddressRepository>().As<IAddressrepository>().InstancePerLifetimeScope();
            builder.RegisterType<LookupRepository>().As<ILookupRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HelperRepository>().As<IHelperRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HelperAddressRepository>().As<IHelperAddressRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AgencyAddressRepository>().As<IAgencyAddressRepository>().InstancePerLifetimeScope();
            builder.RegisterType<Sexualityrepository>().As<ISexualityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageRepository>().As<ILanguageRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GenderRepository>().As<IGenderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RaceEthnicityRepository>().As<IRaceEthnicityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<IdentificationTypeRepository>().As<IIdentificationTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CollaborationRepository>().As<ICollaborationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SupportTypeRepository>().As<ISupportTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonAddressRepository>().As<IPersonAddressRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonCollaborationRepository>().As<IPersonCollaborationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonIdentificationRepository>().As<IPersonIdentificationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonLanguageRepository>().As<IPersonLanguageRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonRaceEthnicityRepository>().As<IPersonRaceEthnicityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonSupportRepository>().As<IPersonSupportRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CollaborationRepository>().As<ICollaborationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireRepository>().As<IQuestionnaireRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationObjectTypeRepository>().As<IApplicationObjectTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireItemRepository>().As<IQuestionnaireItemRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireNotifyRiskScheduleRepository>().As<IQuestionnaireNotifyRiskScheduleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireNotifyRiskRuleConditionRepository>().As<IQuestionnaireNotifyRiskRuleConditionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireNotifyRiskRuleRepository>().As<IQuestionnaireNotifyRiskRuleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireWindowRepository>().As<IQuestionnaireWindowRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireReminderRuleRespository>().As<IQuestionnaireReminderRuleRespository>().InstancePerLifetimeScope();
            builder.RegisterType<AuditTableRepository>().As<IAuditTableRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AspNetUserRepository>().As<IAspNetUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TenantProvider>().As<ITenantProvider>().InstancePerLifetimeScope();
            builder.RegisterType<CustomUserStore>().As<IUserStore<AspNetUser>>().InstancePerLifetimeScope();
            builder.RegisterType<CustomRoleStore>().As<IRoleStore<RolesLookup>>().SingleInstance();
            builder.RegisterType<UserManager<AspNetUser>>().As<UserManager<AspNetUser>>();
            builder.RegisterType<SignInManager<AspNetUser>>().As<SignInManager<AspNetUser>>();
            builder.RegisterType<ReportingUnitRepository>().As<IReportingUnitRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AgencySharingRepository>().As<IAgencySharingRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AgencySharingPolicyRepository>().As<IAgencySharingPolicyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HelperTitleRepository>().As<IHelperTitleRepository>().InstancePerLifetimeScope();

            builder.RegisterType<HelperTitleRepository>().As<IHelperTitleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuditTableRepository>().As<IAuditTableRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ReportingUnitRepository>().As<IReportingUnitRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SharingPolicyRepository>().As<ISharingPolicyRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AuditTableRepository>().As<IAuditTableRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CollaborationSharingRepository>().As<ICollaborationSharingRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CollaborationSharingPolicyRepository>().As<ICollaborationSharingPolicyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AssessmentRepository>().As<IAssessmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AssessmentResponseRepository>().As<IAssessmentResponseRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonQuestionnaireRepository>().As<IPersonQuestionnaireRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CollaborationLevelRepository>().As<ICollaborationLevelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationlevelRepository>().As<INotificationLevelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CollaborationTagTypeRepository>().As<ICollaborationTagTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TherapyTypeRepository>().As<ITherapyTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HelperTitleRepository>().As<IHelperTitleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SystemRoleRepository>().As<ISystemRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AssessmentReasonRepository>().As<IAssessmentReasonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<VoiceTypeRepository>().As<IVoiceTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationTypeRepository>().As<INotificationTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NoteRepository>().As<INoteRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationLogRepository>().As<INotificationLogRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AssessmentResponseNoteRepository>().As<IAssessmentResponseNoteRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AssessmentStatusRepository>().As<IAssessmentStatusRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SystemRoleRepository>().As<ISystemRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonQuestionnaireMetricsRepository>().As<IPersonQuestionnaireMetricsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SearchRepository>().As<ISearchRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ReportRepository>().As<IReportRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FileRepository>().As<IFileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<IdentifiedGenderRepository>().As<IIdentifiedGenderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PersonQuestionnaireScheduleRepository>().As<IPersonQuestionnaireScheduleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireReminderTypeRepository>().As<IQuestionnaireReminderTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NotifyReminderRepository>().As<INotifyReminderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExportImportRepository>().As<IExportImportRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ImportRepository>().As<IImportRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AssessmentResponseAttachmentRepository>().As<IAssessmentResponseAttachmentRepository>().InstancePerLifetimeScope();

            // registering other interface items
            //builder.RegisterType<RedisCache>().As<ICache>().InstancePerLifetimeScope();
            builder.RegisterType<Queue.AzureStorageQueue>().As<IQueue>().InstancePerLifetimeScope();
            builder.RegisterType<Utility>().As<IUtility>().InstancePerLifetimeScope();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterType<DataProtection>().As<IDataProtection>().InstancePerLifetimeScope();
            builder.RegisterType<QueryBuilder>().As<IQueryBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<SMSSender>().As<ISMSSender>().InstancePerLifetimeScope();
            builder.RegisterType<AuditPersonProfileRepository>().As<IAuditPersonProfileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AzureADB2CService>().As<IAzureADB2CService>().InstancePerLifetimeScope();
        }
    }
}
