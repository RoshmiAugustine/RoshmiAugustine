// -----------------------------------------------------------------------
// <copyright file="IAgencyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface ILookupRepository
    {
        /// <summary>
        /// To add agent details.
        /// </summary>
        /// <returns> CountryStateDTO.</returns>
        Task<List<CountryStateDTO>> GetAllState();

        /// <summary>
        /// GetAllRolesLookup.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        List<RoleLookupDTO> GetAllRolesLookup();

        /// <summary>
        /// GetAllHelperLookup.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        List<HelperLookupDTO> GetAllHelperLookup();

        /// <summary>
        /// GetAllItemResponseBehavior.
        /// </summary>
        /// <returns>ItemResponseBehaviorResponseDTO.</returns>
        List<ItemResponseBehaviorDTO> GetAllItemResponseBehavior();

        /// <summary>
        /// GetAllItemResponseType.
        /// </summary>
        /// <returns>ItemResponseTypeDTO.</returns>
        List<ItemResponseTypeDTO> GetAllItemResponseType();

        /// <summary>
        /// To get all type of Collaboration.
        /// </summary>
        /// <returns> CollaborationTypesResponseDTO.</returns>
        List<CollaborationDataDTO> GetAllCollaboration();


        /// <summary>
        /// GetAllNotificationLevel.
        /// </summary>
        /// <returns>NotificationLevelResponseDTO.</returns>
        List<NotificationLevelDTO> GetAllNotificationLevel();

        /// <summary>
        /// GetAllQuestionnaireItems.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        List<QuestionnaireItemDTO> GetAllQuestionnaireItems(int id);

        /// <summary>
        /// GetAllQuestionnaire.
        /// </summary>
        /// <returns>QuestionnaireDTO.</returns>
        List<QuestionnaireDTO> GetAllQuestionnaire();

        /// <summary>
        /// GetAllCategories.
        /// </summary>
        /// <returns>CollaborationTagTypeDTO List.</returns>
        List<CollaborationTagTypeDTO> GetAllCategories();

        /// <summary>
        /// GetAllLeads.
        /// </summary>
        /// <returns>HelperLookupDTO List.</returns>
        List<HelperLookupDTO> GetAllLeads();

        /// <summary>
        /// GetAllLevels.
        /// </summary>
        /// <returns>CollaborationLevelDTO List.</returns>
        List<CollaborationLevelDTO> GetAllLevels();

        /// <summary>
        /// GetAllTherapyTypes.
        /// </summary>
        /// <returns>TherapyTypeDTO List.</returns>
        List<TherapyTypeDTO> GetAllTherapyTypes();

        /// <summary>
        /// GetAllResponse.
        /// </summary>
        /// <returns>ItemResponseLookupDTO.</returns>
        List<ResponseDTO> GetAllResponse();

        /// <summary>
        /// GetCollaborationLookupForOrgAdmin.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationDataDTO List.</returns>
        List<CollaborationDataDTO> GetCollaborationLookupForOrgAdmin(long agencyID, bool activeCollaborations = true);

        /// <summary>
        /// GetAllAgencyLeads.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>HelperLookupDTO List.</returns>
        List<HelperLookupDTO> GetAllAgencyLeads(long agencyID, bool activeHelpers = true);

        /// <summary>
        /// GetCollaborationLevelList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of CollaborationLevelDTO.</returns>
        List<CollaborationLevelDTO> GetCollaborationLevelLookup(long agencyID);
        List<CollaborationLevelDTO> GetCollaborationLevels(long agencyID);

        /// <summary>
        /// Get the Therapy Types list
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>TherapyTypeDTO</returns>
        List<TherapyTypeDTO> GetAgencyTherapyTypeList(long agencyID);

        List<TherapyTypeDTO> GetAgencyTherapyTypes(long agencyID);

        /// <summary>
        /// GetAllHelperLookup.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>RoleLookupResponseDTO.</returns>
        List<HelperLookupDTO> GetAllAgencyHelperLookup(long agencyID, bool activeHelpers = true);

        /// <summary>
        /// GetAgencyNotificationLevelList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of NotificationLevelDTO.</returns>
        List<NotificationLevelDTO> GetAgencyNotificationLevelList(long agencyID);

        /// <summary>
        /// GetAllAgencyQuestionnaire.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>QuestionnaireDTO.</returns>
        List<QuestionnaireDTO> GetAllAgencyQuestionnaire(long agencyID);

        /// <summary>
        /// Get the Collaboration Tag Types list with agency.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationTagTypeDTO</returns>
        List<CollaborationTagTypeDTO> GetAgencyTagTypeList(long agencyID);
        List<CollaborationTagTypeDTO> GetAgencyTagTypes(long agencyID);

        /// <summary>
        /// Get All countries.
        /// </summary>
        /// <returns>CountryLookupDTO list</returns>
        List<CountryLookupDTO> GetAllCountries();

        TimeFrame GetTimeFrameDetails(int daysInService);

        ItemResponseBehaviorDTO GetItemResponseBehaviorById(int id);

        ItemResponseType GetItemResponseTypeById(int id);

        /// <returns>AssessmentsDTO List.</returns>
        List<AssessmentsDTO> GetAllAssessments(int personQuestionnaireID);

        /// <summary>
        /// GetAllAssessments.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="voiceTypeID">voiceTypeID.</param>
        /// <returns>AssessmentsDTO List.</returns>
        List<AssessmentsDTO> GetAllAssessments(long PersonID, long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs);

        /// <summary>
        /// GetAllHelperByAgencyID
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        List<Helper> GetAllHelperByAgencyID(long agencyID);

        /// <summary>
        /// GetAllQuestionnaireItems.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        List<SkipLogicItemsDTO> GetCategoryAndItemforSkipLogic(int questionnireId);

        /// <summary>
        /// GetAllActionType.
        /// </summary>
        /// <returns>ActionTypeDTO.</returns>
        List<ActionTypeDTO> GetAllActionType();
        List<OffsetTypeDTO> GetAllOffsetType();
        List<RecurrenceDayDTO> GetAllRecurrenceDay();
        List<RecurrenceEndTypeDTO> GetAllRecurrenceEndType();
        List<RecurrenceMonthDTO> GetAllRecurrenceMonth();
        List<RecurrenceOrdinalDTO> GetAllRecurrenceOrdinal();
        List<RecurrencePatternDTO> GetAllRecurrencePattern();
        List<TimeZonesDTO> GetAllTimeZones();
        List<InviteToCompleteReceiverDTO> GetAllInviteToCompleteReceivers();
        List<ResponseValueTypeDTO> GetAllResponseValueTypes();
    }
}
