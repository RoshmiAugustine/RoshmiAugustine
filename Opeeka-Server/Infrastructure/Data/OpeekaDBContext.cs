// -----------------------------------------------------------------------
// <copyright file="OpeekaDBContext.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Opeeka.PICS.Infrastructure.Audit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Opeeka.PICS.Infrastructure.Data
{
    public class OpeekaDBContext : IdentityDbContext<AspNetUser>
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _accessor;
        public OpeekaDBContext(DbContextOptions<OpeekaDBContext> options, IConfiguration config, IHttpContextAccessor accessor) : base(options)
        {
        }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AuditDetails> Audits { get; set; }
        public DbSet<AgencyAddress> AgencyAddresses { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Helper> Helper { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<HelperAddress> HelperAddress { get; set; }
        public DbSet<CountryState> CountryStates { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Sexuality> Sexualities { get; set; }
        public DbSet<RaceEthnicity> RaceEthnicities { get; set; }
        public DbSet<IdentifiedGender> IdentifiedGender { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<IdentificationType> identificationTypes { get; set; }
        public DbSet<SupportType> SupportTypes { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonAddress> personAddresses { get; set; }
        public DbSet<PersonHelper> PersonHelpers { get; set; }
        public DbSet<PersonLanguage> personLanguages { get; set; }
        public DbSet<PersonIdentification> personIdentifications { get; set; }
        public DbSet<PersonRaceEthnicity> personRaceEthnicities { get; set; }
        public DbSet<PersonCollaboration> PersonCollaborations { get; set; }
        public DbSet<PersonAddress> PersonAddress { get; set; }
        public DbSet<Collaboration> Collaborations { get; set; }
        public DbSet<CollaborationQuestionnaire> CollaborationQuestionnaire { get; set; }
        public DbSet<PersonSupport> personSupports { get; set; }
        public DbSet<PersonIdentification> PersonIdentification { get; set; }
        public DbSet<PersonLanguage> PersonLanguage { get; set; }

        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<CollaborationLeadHistory> CollaborationLeadHistory { get; set; }
        public DbSet<CollaborationTag> CollaborationTag { get; set; }
        public DbSet<CollaborationTagType> CollaborationTagType { get; set; }
        public DbSet<CollaborationLevel> CollaborationLevel { get; set; }
        public DbSet<TherapyType> TherapyType { get; set; }
        public DbSet<PersonRaceEthnicity> PersonRaceEthnicity { get; set; }
        public DbSet<PersonSupport> PersonSupport { get; set; }
        public DbSet<ApplicationObjectType> ApplicationObjectType { get; set; }
        public DbSet<QuestionnaireItem> QuestionnaireItems { get; set; }
        public DbSet<ItemResponseBehavior> ItemResponseBehaviors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemResponseType> ItemResponseTypes { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<QuestionnaireNotifyRiskSchedule> QuestionnaireNotifyRiskSchedules { get; set; }
        public DbSet<QuestionnaireNotifyRiskRule> QuestionnaireNotifyRiskRules { get; set; }
        public DbSet<QuestionnaireNotifyRiskRuleCondition> QuestionnaireNotifyRiskRuleConditions { get; set; }
        public DbSet<NotificationLevel> NotificationLevels { get; set; }
        public DbSet<QuestionnaireWindow> QuestionnaireWindows { get; set; }
        public DbSet<QuestionnaireReminderRule> QuestionnaireReminderRules { get; set; }
        public DbSet<QuestionnaireReminderType> QuestionnaireReminderTypes { get; set; }
        public DbSet<AssessmentReason> AssessmentReasons { get; set; }
        public DbSet<ApplicationObject> ApplicationObjects { get; set; }
        public DbSet<ApplicationObjectType> ApplicationObjectTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<HelperTitle> HelperTitles { get; set; }
        public DbSet<ConfigurationValueType> ConfigurationValueType { get; set; }
        public DbSet<ConfigurationParameter> ConfigurationParameter { get; set; }
        public DbSet<ConfigurationContext> ConfigurationContext { get; set; }
        public DbSet<ConfigurationParameterContext> ConfigurationParameterContext { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<ConfigurationAttachment> ConfigurationAttachment { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<ReportingUnit> ReportingUnits { get; set; }
        public DbSet<AgencySharing> AgencySharings { get; set; }
        public DbSet<AgencySharingPolicy> AgencySharingPolicys { get; set; }
        public DbSet<SharingPolicy> SharingPolicys { get; set; }
        public DbSet<CollaborationSharing> CollaborationSharings { get; set; }
        public DbSet<CollaborationSharingPolicy> CollaborationSharingPolicys { get; set; }
        public DbSet<AuditTableName> AuditTableName { get; set; }
        public DbSet<AuditFieldName> AuditFieldName { get; set; }
        public DbSet<ColorPalette> ColorPalettes { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentStatus> AssessmentStatuses { get; set; }
        public DbSet<PersonQuestionnaire> PersonQuestionnaires { get; set; }
        public DbSet<AssessmentResponse> AssessmentResponses { get; set; }
        public DbSet<AssessmentResponseNote> AssessmentResponseNotes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NotificationLog> NotificationLog { get; set; }
        public DbSet<NotificationResolutionNote> NotificationResolutionNote { get; set; }
        public DbSet<NotifyRisk> NotifyRisk { get; set; }
        public DbSet<NotifyReminder> NotifyReminder { get; set; }
        public DbSet<NotificationResolutionStatus> NotificationResolutionStatus { get; set; }
        public DbSet<CategoryFocus> CategoryFocus { get; set; }
        public DbSet<PersonScreeningStatus> PersonScreeningStatus { get; set; }
        public DbSet<VoiceType> VoiceTypes { get; set; }
        public DbSet<AssessmentResponseText> AssessmentResponseTexts { get; set; }
        public DbSet<ReviewerHistory> ReviewerHistories { get; set; }
        public DbSet<AssessmentNote> AssessmentNotes { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<QuestionnaireItemHistory> QuestionnaireItemHistorys { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<ResponseValueType> ResponseValueTypes { get; set; }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<RolesLookup> RolesLookup { get; set; }
        public DbSet<SystemRolePermission> SystemRolePermission { get; set; }
        public DbSet<NotificationResolutionHistory> NotificationResolutionHistory { get; set; }
        public DbSet<CollaborationSharingHistory> CollaborationSharingHistorys { get; set; }
        public DbSet<CollaborationAgencyAddress> CollaborationAgencyAddresses { get; set; }
        public DbSet<AgencySharingHistory> AgencySharingHistorys { get; set; }
        public DbSet<InstrumentAgency> InstrumentAgencys { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<SupportAddress> SupportAddresses { get; set; }
        public DbSet<SupportContact> SupportContacts { get; set; }
        public DbSet<NotificationMode> NotificationModes { get; set; }
        public DbSet<NotifyRiskRule> NotifyRiskRules { get; set; }
        public DbSet<NotifyRiskValue> NotifyRiskValues { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<NotificationDelivery> NotificationDeliverys { get; set; }
        public DbSet<PersonQuestionnaireMetrics> PersonQuestionnaireMetrics { get; set; }
        public DbSet<PersonQuestionnaireSchedule> PersonQuestionnaireSchedules { get; set; }
        public DbSet<TimeFrame> TimeFrames { get; set; }
        public DbSet<AssessmentEmailOtp> AssessmentEmailOtps { get; set; }
        public DbSet<BackgroundProcessLog> BackgroundProcessLog { get; set; }
        public DbSet<SharingRolePermission> SharingRolePermission { get; set; }
        public DbSet<EmailDetail> EmailDetail { get; set; }
        public DbSet<ImportType> ImportType { get; set; }
        public DbSet<QuestionnaireDefaultResponseRule> QuestionnaireDefaultResponseRules { get; set; }
        public DbSet<QuestionnaireDefaultResponseRuleAction> QuestionnaireDefaultResponseRuleActions { get; set; }
        public DbSet<QuestionnaireDefaultResponseRuleCondition> QuestionnaireDefaultResponseRuleConditions { get; set; }

        public DbSet<PersonAssessmentMetrics> PersonAssessmentMetrics { get; set; }
        public DbSet<AuditPersonProfile> AuditPersonProfile { get; set; }

        public DbSet<QuestionnaireRegularReminderRecurrence> QuestionnaireRegularReminderRecurrences { get; set; }
        public DbSet<QuestionnaireRegularReminderTimeRule> QuestionnaireRegularReminderTimeRules { get; set; }
        public DbSet<InviteToCompleteReceiver> InviteToCompleteReceivers { get; set; }
        public DbSet<OffsetType> OffsetTypes { get; set; }
        public DbSet<TimeZones> TimeZones { get; set; }
        public DbSet<RecurrenceDay> RecurrenceDays { get; set; }
        public DbSet<RecurrenceEndType> RecurrenceEndTypes { get; set; }
        public DbSet<RecurrenceMonth> RecurrenceMonths { get; set; }
        public DbSet<RecurrenceOrdinal> RecurrenceOrdinals { get; set; }
        public DbSet<RecurrencePattern> RecurrencePatterns { get; set; }
        public DbSet<ReminderInviteToComplete> ReminderInviteToCompletes { get; set; }
        public DbSet<AgencyPowerBIReport> AgencyPowerBIReports { get; set; }
        public DbSet<AssessmentResponseAttachment> AssessmentResponseAttachments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AspNetUser>(builder =>
            {
                builder.Metadata.RemoveIndex(new[] { builder.Property(u => u.NormalizedUserName).Metadata });
                builder.HasIndex(u => new { u.NormalizedUserName, u.AgencyId }).HasName("UserNameIndex").IsUnique();
                builder.ToTable("IdentityUsers");
            });
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().ToTable("IdentityRole");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().ToTable("IdentityRoleClaim");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>().ToTable("IdentityUserClaim");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>().ToTable("IdentityUserLogin");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().ToTable("IdentityUserRole");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>().ToTable("IdentityUserToken");

            builder.ApplyConfigurationsFromAssembly(typeof(OpeekaDBContext).Assembly);

        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AuditHelper helper = new AuditHelper(this, _config, _accessor);
            var auditEntries = helper.OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            if (auditEntries != null)
            {
                await helper.OnAfterSaveChanges(auditEntries);
            }

            return result;
        }
    }
}