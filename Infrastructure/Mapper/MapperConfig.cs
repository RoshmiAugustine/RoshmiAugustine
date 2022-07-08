// -----------------------------------------------------------------------
// <copyright file="MapperConfig.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Infrastructure.Infrastructure.Mapper
{
    using Autofac;
    using AutoMapper;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.ExternalAPI;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="MapperConfig" />.
    /// </summary>
    public static class MapperConfig
    {
        /// <summary>
        /// Initializing Auto Mapper.
        /// </summary>
        /// <param name="builder">ContainerBuilder.</param>
        public static void Initialize(ContainerBuilder builder)
        {
            builder.Register(
                 c => new MapperConfiguration(cfg =>
                 {
                     cfg.CreateMap<UsersDTO, User>().ReverseMap();
                     cfg.CreateMap<Task<UsersDTO>, Task<User>>().ReverseMap();
                     cfg.CreateMap<UserRoleDTO, UserRole>().ReverseMap();
                     cfg.CreateMap<Task<UserRoleDTO>, Task<UserRole>>().ReverseMap();
                     cfg.CreateMap<HelperDTO, Helper>().ReverseMap();
                     cfg.CreateMap<Task<HelperDTO>, Task<Helper>>().ReverseMap();
                     cfg.CreateMap<AddressDTO, Address>().ReverseMap();
                     cfg.CreateMap<Task<AddressDTO>, Task<Address>>().ReverseMap();
                     cfg.CreateMap<HelperAddressDTO, HelperAddress>().ReverseMap();
                     cfg.CreateMap<Task<HelperAddressDTO>, Task<HelperAddress>>().ReverseMap();
                     // cfg.CreateMap<WeatherForecastDTO, WeatherForecast>().ReverseMap();
                     //  cfg.CreateMap<Task<WeatherForecastDTO>, Task<WeatherForecast>>().ReverseMap();
                     cfg.CreateMap<AgencyDTO, Agency>().ReverseMap();
                     cfg.CreateMap<Task<AgencyDTO>, Task<Agency>>().ReverseMap();
                     cfg.CreateMap<AddressDTO, Address>().ReverseMap();
                     cfg.CreateMap<Task<AddressDTO>, Task<Address>>().ReverseMap();
                     cfg.CreateMap<AgencyAddressDTO, AgencyAddress>().ReverseMap();
                     cfg.CreateMap<Task<AgencyAddressDTO>, Task<AgencyAddress>>().ReverseMap();
                     cfg.CreateMap<AgencyAddress, AgencyAddressDTO>().ReverseMap();
                     cfg.CreateMap<Task<Agency>, Task<AgencyDTO>>().ReverseMap();
                     cfg.CreateMap<Agency, AgencyDTO>().ReverseMap();
                     cfg.CreateMap<Task<AgencyAddress>, Task<AgencyAddressDTO>>().ReverseMap();
                     cfg.CreateMap<Address, AddressDTO>().ReverseMap();
                     cfg.CreateMap<Task<Address>, Task<AddressDTO>>().ReverseMap();
                     cfg.CreateMap<CountryStateDTO, CountryState>().ReverseMap();
                     cfg.CreateMap<Task<CountryStateDTO>, Task<CountryState>>().ReverseMap();
                     cfg.CreateMap<GenderDTO, Gender>().ReverseMap();
                     cfg.CreateMap<IdentifiedGenderDTO, IdentifiedGender>().ReverseMap();
                     cfg.CreateMap<Task<GenderDTO>, Task<Gender>>().ReverseMap();
                     cfg.CreateMap<SexualityDTO, Sexuality>().ReverseMap();
                     cfg.CreateMap<Task<SexualityDTO>, Task<Sexuality>>().ReverseMap();
                     cfg.CreateMap<LanguageDTO, Language>().ReverseMap();
                     cfg.CreateMap<Task<LanguageDTO>, Task<Language>>().ReverseMap();
                     cfg.CreateMap<RaceEthnicityDTO, RaceEthnicity>().ReverseMap();
                     cfg.CreateMap<Task<RaceEthnicityDTO>, Task<RaceEthnicity>>().ReverseMap();
                     cfg.CreateMap<IdentificationTypeDTO, IdentificationType>().ReverseMap();
                     cfg.CreateMap<Task<IdentificationTypeDTO>, Task<IdentificationType>>().ReverseMap();
                     cfg.CreateMap<SupportTypeDTO, SupportType>().ReverseMap();
                     cfg.CreateMap<Task<SupportTypeDTO>, Task<SupportType>>().ReverseMap();
                     cfg.CreateMap<PeopleDTO, Person>().ReverseMap();
                     cfg.CreateMap<Task<PeopleDTO>, Task<Person>>().ReverseMap();
                     cfg.CreateMap<PersonAddressDTO, PersonAddress>().ReverseMap();
                     cfg.CreateMap<Task<PersonAddressDTO>, Task<PersonAddress>>().ReverseMap();
                     cfg.CreateMap<PersonHelperDTO, PersonHelper>().ReverseMap();
                     cfg.CreateMap<Task<PersonHelperDTO>, Task<PersonHelper>>().ReverseMap();
                     cfg.CreateMap<PersonCollaborationDTO, PersonCollaboration>().ReverseMap();
                     cfg.CreateMap<Task<PersonCollaborationDTO>, Task<PersonCollaboration>>().ReverseMap();
                     cfg.CreateMap<PersonIdentificationDTO, PersonIdentification>().ReverseMap();
                     cfg.CreateMap<Task<PersonIdentificationDTO>, Task<PersonIdentification>>().ReverseMap();
                     cfg.CreateMap<PersonLanguageDTO, PersonLanguage>().ReverseMap();
                     cfg.CreateMap<Task<PersonLanguageDTO>, Task<PersonLanguage>>().ReverseMap();
                     cfg.CreateMap<PersonRaceEthnicityDTO, PersonRaceEthnicity>().ReverseMap();
                     cfg.CreateMap<Task<PersonRaceEthnicityDTO>, Task<PersonRaceEthnicity>>().ReverseMap();
                     cfg.CreateMap<PersonSupportDTO, PersonSupport>().ReverseMap();
                     cfg.CreateMap<Task<PersonSupportDTO>, Task<PersonSupport>>().ReverseMap();
                     cfg.CreateMap<PersonHelper, PersonHelperDTO>().ReverseMap();
                     cfg.CreateMap<Task<PersonHelper>, Task<PersonHelperDTO>>().ReverseMap();
                     cfg.CreateMap<PersonCollaboration, PersonCollaborationDTO>().ReverseMap();
                     cfg.CreateMap<Task<PersonCollaboration>, Task<PersonCollaborationDTO>>().ReverseMap();
                     cfg.CreateMap<PersonIdentification, PersonIdentificationDTO>().ReverseMap();
                     cfg.CreateMap<Task<PersonIdentification>, Task<PersonIdentificationDTO>>().ReverseMap();
                     cfg.CreateMap<PersonLanguageDTO, PersonLanguage>().ReverseMap();
                     cfg.CreateMap<Task<PersonLanguage>, Task<PersonLanguageDTO>>().ReverseMap();
                     cfg.CreateMap<PersonLanguage, PersonLanguageDTO>().ReverseMap();
                     cfg.CreateMap<PersonRaceEthnicity, PersonRaceEthnicityDTO>().ReverseMap();
                     cfg.CreateMap<Task<PersonRaceEthnicity>, Task<PersonRaceEthnicityDTO>>().ReverseMap();
                     cfg.CreateMap<PersonSupport, PersonSupportDTO>().ReverseMap();
                     cfg.CreateMap<Task<PersonSupport>, Task<PersonSupportDTO>>().ReverseMap();
                     cfg.CreateMap<CollaborationDTO, Collaboration>().ReverseMap();
                     cfg.CreateMap<CollaborationQuestionnaireDTO, CollaborationQuestionnaire>().ReverseMap();
                     cfg.CreateMap<PersonAddressDTO, PersonAddress>().ReverseMap();
                     cfg.CreateMap<Task<PersonAddressDTO>, Task<PersonAddress>>().ReverseMap();
                     cfg.CreateMap<CollaborationLeadHistoryDTO, CollaborationLeadHistory>().ReverseMap();
                     cfg.CreateMap<CollaborationTagDTO, CollaborationTag>().ReverseMap();
                     cfg.CreateMap<ApplicationObjectTypeDTO, ApplicationObjectType>().ReverseMap();
                     cfg.CreateMap<Task<ApplicationObjectTypeDTO>, Task<ApplicationObjectType>>().ReverseMap();
                     cfg.CreateMap<Questionnaire, QuestionnairesDTO>().ReverseMap();
                     cfg.CreateMap<Task<Questionnaire>, Task<QuestionnairesDTO>>().ReverseMap();
                     cfg.CreateMap<QuestionnairesDTO, Questionnaire>().ReverseMap();
                     cfg.CreateMap<Task<QuestionnairesDTO>, Task<Questionnaire>>().ReverseMap();
                     cfg.CreateMap<QuestionnaireItemsDTO, QuestionnaireItem>().ReverseMap();
                     cfg.CreateMap<Task<QuestionnaireItemsDTO>, Task<QuestionnaireItem>>().ReverseMap();
                     cfg.CreateMap<QuestionnaireWindow, QuestionnaireWindowDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireWindowDTO, QuestionnaireWindow>().ReverseMap();

                     cfg.CreateMap<QuestionnaireReminderRule, QuestionnaireReminderRuleDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireReminderRuleDTO, QuestionnaireReminderRule>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskSchedule, QuestionnaireNotifyRiskScheduleDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskScheduleDTO, QuestionnaireNotifyRiskSchedule>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskRule, QuestionnaireNotifyRiskRuleDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskRuleDTO, QuestionnaireNotifyRiskRule>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskRuleCondition, QuestionnaireNotifyRiskRuleConditionDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskRuleConditionDTO, QuestionnaireNotifyRiskRuleCondition>().ReverseMap();
                     cfg.CreateMap<CollaborationLevelDTO, CollaborationLevel>().ReverseMap();
                     cfg.CreateMap<NotificationLevelDTO, NotificationLevel>().ReverseMap();

                     cfg.CreateMap<LanguageDetailsDTO, Language>().ReverseMap();
                     cfg.CreateMap<Task<LanguageDetailsDTO>, Task<Language>>().ReverseMap();

                     cfg.CreateMap<SystemRoleDTO, SystemRole>().ReverseMap();
                     cfg.CreateMap<Task<SystemRoleDTO>, Task<SystemRole>>().ReverseMap();
                     cfg.CreateMap<ReportingUnitDTO, ReportingUnit>().ReverseMap();
                     cfg.CreateMap<Task<ReportingUnitDTO>, Task<ReportingUnit>>().ReverseMap();
                     cfg.CreateMap<AgencySharingDTO, AgencySharing>().ReverseMap();
                     cfg.CreateMap<Task<AgencySharingDTO>, Task<AgencySharing>>().ReverseMap();
                     cfg.CreateMap<AgencySharingPolicyDTO, AgencySharingPolicy>().ReverseMap();
                     cfg.CreateMap<Task<AgencySharingPolicyDTO>, Task<AgencySharingPolicy>>().ReverseMap();
                     cfg.CreateMap<HelperTitleDTO, HelperTitle>().ReverseMap();

                     cfg.CreateMap<ReportingUnitDTO, ReportingUnit>().ReverseMap();
                     cfg.CreateMap<Task<ReportingUnitDTO>, Task<ReportingUnit>>().ReverseMap();
                     cfg.CreateMap<SharingPolicy, SharingPolicyDTO>().ReverseMap();
                     cfg.CreateMap<Task<SharingPolicy>, Task<SharingPolicyDTO>>().ReverseMap();
                     cfg.CreateMap<Task<HelperTitleDTO>, Task<HelperTitle>>().ReverseMap();
                     cfg.CreateMap<CollaborationSharing, CollaborationSharingDTO>().ReverseMap();
                     cfg.CreateMap<Task<CollaborationSharing>, Task<CollaborationSharingDTO>>().ReverseMap();
                     cfg.CreateMap<CollaborationSharingDTO, CollaborationSharing>().ReverseMap();
                     cfg.CreateMap<Task<CollaborationSharingDTO>, Task<CollaborationSharing>>().ReverseMap();
                     cfg.CreateMap<CollaborationSharingPolicy, CollaborationSharingPolicyDTO>().ReverseMap();
                     cfg.CreateMap<Task<CollaborationSharingPolicy>, Task<CollaborationSharingPolicyDTO>>().ReverseMap();
                     cfg.CreateMap<PersonQuestionnaireDTO, PersonQuestionnaire>().ReverseMap();
                     cfg.CreateMap<Task<PersonQuestionnaireDTO>, Task<PersonQuestionnaire>>().ReverseMap();
                     cfg.CreateMap<PersonQuestionnaire, PersonQuestionnaireDTO>().ReverseMap();
                     cfg.CreateMap<Task<PersonQuestionnaire>, Task<PersonQuestionnaireDTO>>().ReverseMap();
                     cfg.CreateMap<CollaborationTagTypeDTO, CollaborationTagType>().ReverseMap();
                     cfg.CreateMap<Task<CollaborationTagTypeDTO>, Task<CollaborationTagType>>().ReverseMap();
                     cfg.CreateMap<TherapyType, TherapyTypeDTO>().ReverseMap();
                     cfg.CreateMap<Task<TherapyType>, Task<TherapyTypeDTO>>().ReverseMap();
                     cfg.CreateMap<PersonQuestionnaire, PersonQuestionnaireDTO>().ReverseMap();
                     cfg.CreateMap<Task<PersonQuestionnaire>, Task<PersonQuestionnaireDTO>>().ReverseMap();
                     cfg.CreateMap<QuestionnaireReminderRule, QuestionnaireReminderRulesDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireReminderRulesDTO, QuestionnaireReminderRule>().ReverseMap();
                     cfg.CreateMap<QuestionnaireWindow, QuestionnaireWindowsDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireWindowsDTO, QuestionnaireWindow>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskSchedule, QuestionnaireNotifyRiskSchedulesDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskSchedulesDTO, QuestionnaireNotifyRiskSchedule>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskRule, QuestionnaireNotifyRiskRulesDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskRulesDTO, QuestionnaireNotifyRiskRule>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskRuleCondition, QuestionnaireNotifyRiskRuleConditionsDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireNotifyRiskRuleConditionsDTO, QuestionnaireNotifyRiskRuleCondition>().ReverseMap();
                     cfg.CreateMap<SystemRolePermissionDTO, SystemRolePermission>().ReverseMap();
                     cfg.CreateMap<Task<SystemRolePermissionDTO>, Task<SystemRolePermission>>().ReverseMap();
                     cfg.CreateMap<RolePermissionDTO, RolePermission>().ReverseMap();
                     cfg.CreateMap<Task<RolePermissionDTO>, Task<RolePermission>>().ReverseMap();
                     cfg.CreateMap<AssessmentReasonLookupDTO, AssessmentReason>().ReverseMap();
                     cfg.CreateMap<Task<AssessmentReasonLookupDTO>, Task<AssessmentReason>>().ReverseMap();
                     cfg.CreateMap<VoiceTypeLookupDTO, VoiceType>().ReverseMap();
                     cfg.CreateMap<Task<VoiceTypeLookupDTO>, Task<VoiceType>>().ReverseMap();
                     cfg.CreateMap<NotificationResolutionStatus, NotificationResolutionStatusDTO>().ReverseMap();
                     cfg.CreateMap<Task<NotificationResolutionStatus>, Task<NotificationResolutionStatusDTO>>().ReverseMap();
                     cfg.CreateMap<NotificationLog, NotificationLogDTO>().ReverseMap();
                     cfg.CreateMap<NotificationLogDTO, NotificationLog>().ReverseMap();
                     cfg.CreateMap<NotificationResolutionHistory, NotificationResolutionHistoryDTO>().ReverseMap();
                     cfg.CreateMap<NotificationResolutionHistoryDTO, NotificationResolutionHistory>().ReverseMap();
                     cfg.CreateMap<Note, NoteDTO>().ReverseMap();
                     cfg.CreateMap<NoteDTO, Note>().ReverseMap();
                     cfg.CreateMap<NotificationResolutionNote, NotificationResolutionNoteDTO>().ReverseMap();
                     cfg.CreateMap<NotificationResolutionNoteDTO, NotificationResolutionNote>().ReverseMap();
                     cfg.CreateMap<NotificationTypeDTO, NotificationType>().ReverseMap();
                     cfg.CreateMap<Task<NotificationTypeDTO>, Task<NotificationType>>().ReverseMap();
                     cfg.CreateMap<AssessmentDTO, Assessment>().ReverseMap();
                     cfg.CreateMap<Task<AssessmentDTO>, Task<Assessment>>().ReverseMap();
                     cfg.CreateMap<AssessmentResponsesDTO, AssessmentResponse>().ReverseMap();
                     cfg.CreateMap<Task<AssessmentResponsesDTO>, Task<AssessmentResponse>>().ReverseMap();
                     cfg.CreateMap<NoteDTO, Note>().ReverseMap();
                     cfg.CreateMap<Task<NoteDTO>, Task<Note>>().ReverseMap();
                     cfg.CreateMap<AssessmentResponseNoteDTO, AssessmentResponseNote>().ReverseMap();
                     cfg.CreateMap<Task<AssessmentResponseNoteDTO>, Task<AssessmentResponseNote>>().ReverseMap();
                     cfg.CreateMap<ManagerLookupDTO, Helper>().ReverseMap();
                     cfg.CreateMap<Task<ManagerLookupDTO>, Task<Helper>>().ReverseMap();
                     cfg.CreateMap<AssessmentEmailLinkDetailsDTO, AssessmentEmailLinkDetails>().ReverseMap();
                     cfg.CreateMap<Task<AssessmentEmailLinkDetailsDTO>, Task<AssessmentEmailLinkDetails>>().ReverseMap();
                     cfg.CreateMap<Task<NotificationLog>, Task<NotificationLogDTO>>().ReverseMap();
                     cfg.CreateMap<Response, ResponseDTO>().ReverseMap();
                     cfg.CreateMap<ResponseDTO, Response>().ReverseMap();
                     cfg.CreateMap<Language, LanguageDTO>().ReverseMap();
                     cfg.CreateMap<Task<Language>, Task<LanguageDTO>>().ReverseMap();
                     cfg.CreateMap<ReviewerHistory, AssessmentHistoryDTO>().ReverseMap();
                     cfg.CreateMap<AssessmentNote, AssessmentNoteDTO>().ReverseMap();
                     cfg.CreateMap<Country, CountryLookupDTO>().ReverseMap();
                     cfg.CreateMap<CountryLookupDTO, Country>().ReverseMap();
                     cfg.CreateMap<TimeFrameDTO, TimeFrame>().ReverseMap();
                     cfg.CreateMap<FileDTO, File>().ReverseMap();
                     cfg.CreateMap<UserProfileDTO, UserProfile>().ReverseMap();
                     cfg.CreateMap<AssessmentEmailOtpDTO, AssessmentEmailOtp>().ReverseMap();
                     cfg.CreateMap<AgencySharingHistoryDTO, AgencySharingHistory>().ReverseMap();
                     cfg.CreateMap<CollaborationSharingHistoryDTO, CollaborationSharingHistory>().ReverseMap();
                     cfg.CreateMap<PersonCollaborationDTO, PersonEditCollaborationDTO>().ReverseMap();
                     cfg.CreateMap<PeopleDetailsDTO, PeopleEditDetailsDTO>().ReverseMap();
                     cfg.CreateMap<PeopleEditDetailsDTO, PeopleDetailsDTO>().ReverseMap();
                     cfg.CreateMap<PersonIdentificationDTO, PersonEditIdentificationDTO>().ReverseMap();
                     cfg.CreateMap<PersonRaceEthnicityDTO, PersonEditRaceEthnicityDTO>().ReverseMap();
                     cfg.CreateMap<PersonHelperDTO, PersonEditHelperDTO>().ReverseMap();
                     cfg.CreateMap<PersonHelper, PersonEditHelperDTO>().ReverseMap();
                     cfg.CreateMap<PersonSupportDTO, PersonEditSupportDTO>().ReverseMap();
                     cfg.CreateMap<PersonCollaborationDTO, PersonEditCollaborationDTO>().ReverseMap();
                     cfg.CreateMap<EmailDetailsDTO, EmailDetail>().ReverseMap();
                     cfg.CreateMap<ExportTemplateDTO, ExportTemplate>().ReverseMap();
                     cfg.CreateMap<FileImportDTO, FileImport>().ReverseMap();


                     cfg.CreateMap<QuestionnaireReminderType, QuestionnaireReminderTypeDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireReminderTypeDTO, QuestionnaireReminderType>().ReverseMap();
                     cfg.CreateMap<NotifyReminder, NotifyReminderDTO>().ReverseMap();
                     cfg.CreateMap<NotifyReminderDTO, NotifyReminder>().ReverseMap();

                     cfg.CreateMap<BackgroundProcessLog, BackgroundProcessLogDTO>().ReverseMap();
                     cfg.CreateMap<BackgroundProcessLogDTO, BackgroundProcessLog>().ReverseMap();
                     cfg.CreateMap<NotifyRisk, NotifyRiskDTO>().ReverseMap();
                     cfg.CreateMap<NotifyRiskDTO, NotifyRisk>().ReverseMap(); 
                     cfg.CreateMap<PersonQuestionnaireMetrics, PersonQuestionnaireMetricsDTO>().ReverseMap();
                     cfg.CreateMap<PersonQuestionnaireMetricsDTO, PersonQuestionnaireMetrics>().ReverseMap();                     

                     cfg.CreateMap<PersonQuestionnaireSchedule, PersonQuestionnaireScheduleDTO>().ReverseMap();
                     cfg.CreateMap<PersonQuestionnaireScheduleDTO, PersonQuestionnaireSchedule>().ReverseMap();


                     cfg.CreateMap<QuestionnaireSkipLogicRule, QuestionnaireSkipLogicRuleDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireSkipLogicRuleDTO, QuestionnaireSkipLogicRule>().ReverseMap();

                     cfg.CreateMap<QuestionnaireSkipLogicRuleAction, QuestionnaireSkipLogicRuleActionDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireSkipLogicRuleActionDTO, QuestionnaireSkipLogicRuleAction>().ReverseMap();

                     cfg.CreateMap<QuestionnaireSkipLogicRuleCondition, QuestionnaireSkipLogicRuleConditionDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireSkipLogicRuleConditionDTO, QuestionnaireSkipLogicRuleCondition>().ReverseMap();

                     cfg.CreateMap<ImportTypeDTO, ImportType>().ReverseMap();
                     cfg.CreateMap<QuestionnaireSkipLogicRuleDTO, QuestionnaireSkipLogicRule>().ReverseMap();
                     cfg.CreateMap<QuestionnaireSkipLogicRuleActionDTO, QuestionnaireSkipLogicRuleAction>().ReverseMap();
                     cfg.CreateMap<QuestionnaireSkipLogicRuleConditionDTO, QuestionnaireSkipLogicRuleCondition>().ReverseMap();
                     cfg.CreateMap<PeopleEditDetailsForExternalDTO, PeopleEditDetailsDTO>().ReverseMap();
                     cfg.CreateMap<PersonEditSupportForExternalDTO, PersonEditSupportDTO>().ReverseMap();
                     cfg.CreateMap<PersonEditRaceEthnicityForExternalDTO, PersonEditRaceEthnicityDTO>().ReverseMap();
                     cfg.CreateMap<PersonEditCollaborationForExternalDTO, PersonEditCollaborationDTO>().ReverseMap();
                     cfg.CreateMap<PersonEditHelperForExternalDTO, PersonEditHelperDTO>().ReverseMap();
                     cfg.CreateMap<PersonEditIdentificationForExternalDTO, PersonEditIdentificationDTO>().ReverseMap();
                     cfg.CreateMap<PeopleAddDetailsForExternalDTO, PeopleDetailsDTO>().ReverseMap();
                     cfg.CreateMap<PersonAddSupportForExternalDTO, PersonSupportDTO>().ReverseMap();
                     cfg.CreateMap<PersonAddRaceEthnicityForExternalDTO, PersonRaceEthnicityDTO>().ReverseMap();
                     cfg.CreateMap<PersonAddCollaborationForExternalDTO, PersonCollaborationDTO>().ReverseMap();
                     cfg.CreateMap<PersonAddHelperForExternalDTO, PersonHelperDTO>().ReverseMap();
                     cfg.CreateMap<PersonAddIdentificationForExternalDTO, PersonIdentificationDTO>().ReverseMap(); 
                     cfg.CreateMap<HelperDetailsInputDTO, HelperDetailsDTO>().ReverseMap();
                     cfg.CreateMap<HelperDetailsEditInputDTO, HelperDetailsDTO>().ReverseMap();
                     cfg.CreateMap<CollabrationAddDTOForExternal, CollaborationDetailsDTO>().ReverseMap();
                     cfg.CreateMap<CollaborationQuestionnaireDTOForExternal,CollaborationQuestionnaireDTO>().ReverseMap();
                     cfg.CreateMap<CollaborationTagDTOForExternal, CollaborationTagDTO>().ReverseMap();
                     cfg.CreateMap<CollaborationLeadHistoryDTOForExternal, CollaborationLeadHistoryDTO>().ReverseMap();

                     cfg.CreateMap<CollabrationUpdateDTOForExternal, CollaborationDetailsDTO>().ReverseMap();
                     cfg.CreateMap<CollaborationTagDTOForEditExternal, CollaborationTagDTO>().ReverseMap();
                     cfg.CreateMap<CollaborationQuestionnaireDTOForEditExternal, CollaborationQuestionnaireDTO>().ReverseMap();
                     cfg.CreateMap<CollaborationLeadHistoryDTOForEditExternal, CollaborationLeadHistoryDTO>().ReverseMap();
                     cfg.CreateMap<HelperInfoDTO, HelperDetailsDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireDefaultResponseRuleDTO, QuestionnaireDefaultResponseRule>().ReverseMap();
                     cfg.CreateMap<QuestionnaireDefaultResponseRule, QuestionnaireDefaultResponseRuleDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireDefaultResponseRuleConditionDTO, QuestionnaireDefaultResponseRuleCondition>().ReverseMap();
                     cfg.CreateMap<QuestionnaireDefaultResponseRuleCondition, QuestionnaireDefaultResponseRuleConditionDTO>().ReverseMap(); 
                     cfg.CreateMap<QuestionnaireDefaultResponseRuleActionDTO, QuestionnaireDefaultResponseRuleAction>().ReverseMap();
                     cfg.CreateMap<QuestionnaireDefaultResponseRuleAction, QuestionnaireDefaultResponseRuleActionDTO>().ReverseMap();
                     cfg.CreateMap<PersonAssessmentMetrics, PersonAssessmentMetricsDTO>().ReverseMap();
                     cfg.CreateMap<PeopleDetailsDTO, ValidatePersonDTOForExternal>().ReverseMap();
                     cfg.CreateMap<PeopleEditDetailsDTO, ValidatePersonDTOForExternal>().ReverseMap();
                     cfg.CreateMap<PersonEditIdentificationDTO, PersonIdentificationDTO>().ReverseMap();
                     cfg.CreateMap<PersonEditRaceEthnicityDTO, PersonRaceEthnicityDTO>().ReverseMap();
                     cfg.CreateMap<PersonEditCollaborationDTO, PersonCollaborationDTO>().ReverseMap();
                     cfg.CreateMap<PersonEditHelperDTO, PersonHelperDTO>().ReverseMap();
                     cfg.CreateMap<AgencyInsightDashboard, AgencyInsightDashboardDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireRegularReminderRecurrence, QuestionnaireRegularReminderRecurrenceDTO>().ReverseMap();
                     cfg.CreateMap<QuestionnaireRegularReminderTimeRule, QuestionnaireRegularReminderTimeRuleDTO>().ReverseMap();
                     cfg.CreateMap<TimeZones, TimeZonesDTO>().ReverseMap();
                     cfg.CreateMap<OffsetType, OffsetTypeDTO>().ReverseMap();
                     cfg.CreateMap<RecurrenceDay, RecurrenceDayDTO>().ReverseMap();
                     cfg.CreateMap<RecurrenceEndType, RecurrenceEndTypeDTO>().ReverseMap();
                     cfg.CreateMap<RecurrenceMonth, RecurrenceMonthDTO>().ReverseMap();
                     cfg.CreateMap<RecurrenceOrdinal, RecurrenceOrdinalDTO>().ReverseMap();
                     cfg.CreateMap<RecurrencePattern, RecurrencePatternDTO>().ReverseMap();
                     cfg.CreateMap<InviteToCompleteReceiver, InviteToCompleteReceiverDTO>().ReverseMap();
                     cfg.CreateMap<ReminderInviteToComplete, ReminderInviteToCompleteDTO>().ReverseMap();
                     cfg.CreateMap<AddPersonSupportDTOForExternal, PersonSupportDTO>().ReverseMap();
                     cfg.CreateMap<EditPersonSupportDTOForExternal, PersonSupportDTO>().ReverseMap();
                     cfg.CreateMap<ResponseValueType, ResponseValueTypeDTO>().ReverseMap();
                     cfg.CreateMap<AgencyPowerBIReport, AgencyPowerBIReportDTO>().ReverseMap();
                     cfg.CreateMap<NotifyRiskValue, NotifyRiskValueDTO>().ReverseMap();
                 }))
                 .AsSelf()
                 .SingleInstance();

            builder.Register(
                c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
