// -----------------------------------------------------------------------
// <copyright file="ILookupService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// IAgencyService.
    /// </summary>
    public interface ILookupService
    {
        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="agencyDetailsDTO">id.</param>
        /// <returns>ResultDTO.</returns>
        CountryStateResponseDTO GetAllCountryState();

        /// <summary>
        /// GetAllCountries.
        /// </summary>
        /// <returns>CountryLookupResponseDTO.</returns>
        CountryLookupResponseDTO GetAllCountries();

        /// <summary>
        /// GetAllAgencyLookup
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>AgencyLookupResponseDTO.</returns>
        AgencyLookupResponseDTO GetAllAgencyLookup(long agencyID);

        /// <summary>
        /// GetAllRolesLookup.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        RoleLookupResponseDTO GetAllRolesLookup();

        /// <summary>
        /// GetAllHelperLookup.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>RoleLookupResponseDTO.</returns>
        HelperLookupResponseDTO GetAllHelperLookup(long agencyID);

        /// <summary>
        /// To get all type of genders.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> GenderDTO.</returns>
        GenderResponseDTO GetAllGender(long agencyID);

        /// <summary>
        /// To get all type of language.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> LanguageResponseDTO.</returns>
        LanguageResponseDTO GetAllLanguages(long agencyID);

        /// <summary>
        /// To get all type of races.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> RaceEthnicityResponseDTO.</returns>
        RaceEthnicityResponseDTO GetAllRaceEthnicity(long agencyID);

        /// <summary>
        /// To get all type of sexuality.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> SexualityResponseDTO.</returns>
        SexualityResponseDTO GetAllSexuality(long agencyID);

        /// <summary>
        /// Get all SupportType.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>SupportTypeResponseDTO.<SupportType>.</returns>
        SupportTypeResponseDTO GetAllSupportType(long agencyID);

        /// <summary>
        /// To get all type of IdentificationType.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> IdentificationTypesResponseDTO.</returns>
        IdentificationTypesResponseDTO GetAllIdentificationType(long agencyID);

        /// <summary>
        /// To get all type of sexuality.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns> SexualityResponseDTO.</returns>
        CollaborationTypesResponseDTO GetAllCollaboration(long agencyID);

        /// <summary>
        /// GetAllItemResponseBehavior.
        /// </summary>
        /// <returns>ItemResponseBehaviorResponseDTO.</returns>
        ItemResponseBehaviorResponseDTO GetAllItemResponseBehavior();

        /// <summary>
        /// GetAllItemResponseType.
        /// </summary>
        /// <returns>ItemResponseTypeDTO.</returns>
        ItemResponseTypeResponseDTO GetAllItemResponseType();

        /// <summary>
        /// GetAllNotificationLevel.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        NotificationLevelResponseDTO GetAllNotificationLevel(long agencyID);

        /// <summary>
        /// GetAllQuestionnaireItems.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        public QuestionnaireItemsResponseDTO GetAllQuestionnaireItems(int id);

        /// <summary>
        /// GetAllQuestionnaire.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>QuestionnaireResponseDTO.</returns>
        QuestionnaireResponseDTO GetAllQuestionnaire(long agencyID);

        /// <summary>
        /// GetAllCategories.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CategoryResponseDTO.</returns>
        CategoryResponseDTO GetAllCategories(long agencyID);

        /// <summary>
        /// GetAllLeads.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>HelperLookupResponseDTO.</returns>
        HelperLookupResponseDTO GetAllLeads(long agencyID);

        /// <summary>
        /// GetAllLevels.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationLevelResponseDTO.</returns>
        CollaborationLevelResponseDTO GetAllLevels(long agencyID);

        /// <summary>
        /// GetAllTherapyTypes.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>TherapyTypesResponseDTO.</returns>
        TherapyTypesResponseDTO GetAllTherapyTypes(long agencyID);

        /// <summary>
        /// GetAllResponse.
        /// </summary>
        /// <returns>ItemResponseLookupDTO.</returns>
        ItemResponseLookupDTO GetAllResponse();

        /// <summary>
        /// Get all HelperTitle.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ActionResult.<HelperTitle>.</returns>
        HelperTitleResponseDTO GetAllHelperTitle(long agencyID);

        /// <summary>
        /// Get all GetSharingPolicyList.
        /// </summary>
        /// <returns>ActionResult.<SharingPolicyResponseDTO>.</returns>
        SharingPolicyResponseDTO GetSharingPolicyList();

        /// <summary>
        /// Get all AgencyCollaborationSharing.
        /// </summary>
        /// <returns>ActionResult.<CollaborationSharingResponseDTO>.</returns>
        CollaborationsResponseDTO GetAgencyCollaboration(long id);

        /// <summary>
        /// GetAllSupportsForPerson.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonSupportLookupResponseDTO.</returns>
        PersonSupportLookupResponseDTO GetAllSupportsForPerson(Guid personIndex);

        /// <summary>
        /// GetAllAssessmentReason
        /// </summary>
        /// <returns>AssessmentReasonLookupResponseDTO</returns>
        AssessmentReasonLookupResponseDTO GetAllAssessmentReason();

        /// <summary>
        /// GetAllVoiceType
        /// </summary>
        /// <returns>VoiceTypeLookupResponseDTO</returns>
        VoiceTypeLookupResponseDTO GetAllVoiceType();

        /// <summary>
        /// GetAllVoiceType
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>VoiceTypeLookupResponseDTO</returns>
        VoiceTypeResponseDTO GetAllVoiceTypeInDetail(Guid personIndex, long personQuestionaireID, long personCollaborationID, UserTokenDetails userTokenDetails);

        /// <summary>
        /// GetAllActiveVoiceType Based on Date
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>VoiceTypeLookupResponseDTO</returns>
        VoiceTypeResponseDTO GetActiveVoiceTypeInDetail(Guid personIndex);
        /// <summary>
        /// GetAllNotificationType
        /// </summary>
        /// <returns>NotificationTypeListResponseDTO</returns>
        NotificationTypeListResponseDTO GetAllNotificationType();

        /// <summary>
        /// GetAllManager
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns>ManagerLookupResponseDTO</returns>
        ManagerLookupResponseDTO GetAllManager(long agencyID);

        /// <summary>
        /// GetTimeFrameDetails
        /// </summary>
        /// <param name="daysInService">daysInService</param>
        /// <returns>TimeFrameResponseDTO</returns>
        TimeFrameResponseDTO GetTimeFrameDetails(int daysInService);

        /// <summary>
        /// GetAllAssessments.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <returns>AssessmentsResponseDTO.</returns>
        AssessmentsResponseDTO GetAllAssessments(int personQuestionnaireID);

        /// <summary>
        /// GetAllAssessments.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="voiceTypeID">voiceTypeID.</param>
        /// <returns>AssessmentsResponseDTO.</returns>
        AssessmentsResponseDTO GetAllAssessments(long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID, UserTokenDetails userTokenDetails);

        /// <summary>
        /// GetAllIdentifiedGender.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        IdentifiedGenderResponseDTO GetAllIdentifiedGender(long agencyID);

        /// <summary>
        /// GetVoiceTypeForFilter.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="personQuestionaireID">personQuestionaireID.</param>
        /// <returns>VoiceTypeResponseDTO</returns>
        VoiceTypeResponseDTO GetVoiceTypeForFilter(Guid personIndex, long agencyID, long personQuestionaireID);

        /// <summary>
        /// GetAllAgencySharingPolicy.
        /// </summary>
        /// <returns>AgencySharingPolicyResponseDTO.</returns>
        AgencySharingPolicyResponseDTO GetAllAgencySharingPolicy();

        /// <summary>
        /// GetAllAgencyForSharing.
        /// </summary>
        /// <returns>AgencyLookupResponseDTO.</returns>
        AgencyLookupResponseDTO GetAllAgencyForSharing();

        /// <summary>
        /// GetAllCollaborationSharingPolicy.
        /// </summary>
        /// <returns>CollaborationSharingPolicyResponseDTO.</returns>
        CollaborationSharingPolicyResponseDTO GetAllCollaborationSharingPolicy();

        /// <summary>
        /// GetConfigurationValueByName.
        /// If AgenyID is 0 result will be the applicationLevel configurationValue.
        /// </summary>
        /// <returns>ConfigurationResponseDTO</returns>
        ConfigurationResponseDTO GetConfigurationValueByName(string key, long agencyID);

        /// <summary>
        /// GetAllConfigurationsForAgency.
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns>AllConfigurationsResponseDTO</returns>
        AllConfigurationsResponseDTO GetAllConfigurationsForAgency(long agencyID);

        /// <summary>
        /// GetAllLookupsForEHRAgency
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns>EHRLookupResponseDTO</returns>
        EHRLookupResponseDTO GetAllLookupsForEHRAgency(long agencyID);

        /// <summary>
        /// GetEmailDetails.
        /// </summary>
        /// <returns>EmailDetailsResponseDTO.</returns>
        EmailDetailsResponseDTO GetEmailDetails();

        /// <summary>
        /// UpdateEmailDetails.
        /// </summary>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateEmailDetails(List<EmailDetailsDTO> emailDetails);

        /// <summary>
        /// AddEmailDetails.
        /// </summary>
        /// <param name="emailDetails">emailDetails.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddEmailDetails(List<EmailDetailsDTO> emailDetails);

        /// <summary>
        /// GetAllQuestionnaireReminderType.
        /// </summary>
        /// <returns>QuestionnaireReminderTypeResponseDTO.</returns>
        QuestionnaireReminderTypeResponseDTO GetAllQuestionnaireReminderType();

        /// <summary>
        /// GetIdentificationTypeDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentificationTypesResponseDTO.</returns>
        IdentificationTypesResponseDTO GetIdentificationTypeDetailsByName(string nameCSV, long agencyID);

        /// <summary>
        /// GetRaceEthnicityDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>RaceEthnicityResponseDTO.</returns>
        RaceEthnicityResponseDTO GetRaceEthnicityDetailsByName(string nameCSV, long agencyID);

        /// <summary>
        /// GetAllLookupsForAssessmentUpload.
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        EHRLookupResponseDTO GetAllLookupsForAssessmentUpload(long agencyID, int questionnaireID);

        /// <summary>
        /// GetNotificationType.
        /// </summary>
        /// <param name="type">type.</param>
        /// <returns>NotificationTypeListResponseDTO.</returns>
        NotificationTypeListResponseDTO GetNotificationType(string type);

        /// <summary>
        /// GetIdentifiedGenderDetailsByName.
        /// </summary>
        /// <param name="jsonData">jsonData.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        IdentifiedGenderResponseDTO GetIdentifiedGenderDetailsByName(string jsonData, long agencyID);

        /// <summary>
        /// GetNotificationLevel.
        /// </summary>
        /// <param name="notificationlevelId">notificationlevelId.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        NotificationLevelResponseDTO GetNotificationLevel(int notificationlevelId);

        /// <summary>
        /// GetResponse.
        /// </summary>
        /// <param name="responseId">responseId</param>
        /// <returns>ResponseDetailsDTO.</returns>
        ResponseDetailsDTO GetResponse(int responseId);

        /// <summary>
        /// GetBackgroundProcessLog.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>ResponseDetailsDTO.</returns>
        BackgroundProcessResponseDTO GetBackgroundProcessLog(string name);
        /// <summary>
        /// AddBackgroundProcessLog.
        /// </summary>
        /// <param name="backgroundProcessLog">backgroundProcessLog</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddBackgroundProcessLog(BackgroundProcessLogDTO backgroundProcessLog);

        /// <summary>
        /// UpdateBackgroundProcessLog.
        /// </summary>
        /// <param name="backgroundProcessLog">backgroundProcessLog</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateBackgroundProcessLog(BackgroundProcessLogDTO backgroundProcessLog);

        /// <summary>
        /// GetCategoryAndItemforSkipLogic.
        /// </summary>
        /// <param name="questionnaireId">questionnaireId.</param>
        /// <returns>CategoryAndItemForSkipLogicResponseDTO.</returns>
        CategoryAndItemForSkipLogicResponseDTO GetCategoryAndItemforSkipLogic(int questionnaireId);

        /// <summary>
        /// GetAllActionType.
        /// </summary>
        /// <returns>ActionTypeResponseDTO.</returns>
        ActionTypeResponseDTO GetAllActionType();
        /// <summary>
        /// GetLookupsForRegularReminderSchedules.
        /// </summary>
        /// <returns></returns>
        RegularReminderScheduleLookupDTO GetLookupsForRegularReminderSchedules();
        /// <summary>
        /// AddReminderInviteDetails.
        /// </summary>
        /// <param name="inviteDetails"></param>
        /// <returns></returns>
        CRUDResponseDTO AddReminderInviteDetails(List<ReminderInviteToCompleteDTO> inviteDetails);
        /// <summary>
        /// GetReminderInviteDetails.
        /// </summary>
        /// <returns></returns>
        ReminderInviteToCompleteResponseDTO GetReminderInviteDetails();
        /// <summary>
        /// UpdateReminderInviteDetails.
        /// </summary>
        /// <param name="inviteDetails"></param>
        /// <returns></returns>
        CRUDResponseDTO UpdateReminderInviteDetails(List<ReminderInviteToCompleteDTO> inviteDetails);
        ResponseValueTypeResponseDTO GetAllResponseValueTypeLookup();
    }
}
