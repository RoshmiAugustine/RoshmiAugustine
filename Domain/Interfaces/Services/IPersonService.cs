// -----------------------------------------------------------------------
// <copyright file="IPersonService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// IPersonService.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// GetPersonList.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        GetPersonListDTO GetPersonList(long userID, int pageNumber, int pageSize);

        /// <summary>
        /// GetPeopleDetails
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <returns></returns>
        PeopleDetailsResponseDTO GetPeopleDetails(Guid peopleIndex,long agencyID, int userID);

        /// <summary>
        /// AddPersonDetails.
        /// </summary>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        AddPersonResponseDTO AddPeopleDetails(PeopleDetailsDTO peopleDetailsDTO);


        /// <summary>
        /// RemovePeopleDetails.
        /// </summary>
        /// <param name="peopleIndex">The peopleIndex<see cref="peopleIndex"/>.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO RemovePeopleDetails(Guid peopleIndex, int updateUserID, long agencyID);
        /// <summary>
        ///EditPeopleDetails.
        /// </summary>
        /// <param name="peopleDetailsDTO">The peopleDetailsDTO<see cref="PeopleDetailsDTO"/>.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        CRUDResponseDTO EditPeopleDetails(PeopleEditDetailsDTO peopleEditDetailsDTO, long loggedInAgencyID, string CallingType = "");
        /// <summary>
        /// GetPeopleDetails.
        /// </summary>
        /// <param name="peopleIndex">.</param>
        /// <returns>PeopleDetailsResponseDTO.</returns>
        CRUDResponseDTO AddPersonQuestionaire(PersonQuestionnaireDetailsDTO personQuestionnaireDetailsDTO, long updateUserID, long loggedInAgencyID);

        /// <summary>
        /// GetRiskNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>RiskNotificationDetailsResponseDTO</returns>
        RiskNotificationDetailsResponseDTO GetRiskNotificationList(Guid personIndex);

        /// <summary>
        /// GetPastNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PastNotificationDetailsResponseDTO.</returns>
        PastNotificationDetailsResponseDTO GetPastNotificationList(Guid personIndex);

        /// <summary>
        /// GetAllQuestionnaireForPerson.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>QuestionnairesForPersonDTO.</returns>
        QuestionnairesForPersonDTO GetAllQuestionnaireForPerson(long agencyID, Guid personIndex, int pageNumber, int pageSize);

        /// <summary>
        /// UpdateNotificationStatus.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <param name="status">status.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateNotificationStatus(int notificationLogID, string status, long loggedInAgencyID);

        /// <summary>
        /// GetAllNotes.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PastNotesResponseDTO.</returns>
        PastNotesResponseDTO GetAllPastNotes(int notificationLogID, int pageNumber, int pageSize);

        /// <summary>
        /// AddNotificationNote.
        /// </summary>
        /// <param name="notificationNoteDataDTO">notificationNoteDataDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddNotificationNote(NotificationNoteDataDTO notificationNoteDataDTO, int userID);

        /// <summary>
        /// GetPersonHelpingCount.
        /// </summary>
        /// <param name="helperID">helperID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="roles">roles.</param>
        /// <returns>PersonHelping Count.</returns>
        PersonHelpingResponseDTO GetPersonHelpingCount(int? helperID, long agencyID, List<string> userRoles, bool isSameAsLoggedInUser);

        /// <summary>
        /// GetAllQuestionnaireWithCompletedAssessment.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>AssessedQuestionnairesForPersonDTO.</returns>
        AssessedQuestionnairesForPersonDTO GetAllQuestionnaireWithCompletedAssessment(UserTokenDetails userTokenDetails, Guid personIndex, int pageNumber, int pageSize, long personCollaborationID, int voicetypeID, long voiceTypeFKID);

        /// <summary>
        /// GetPersonsListByHelperID.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="helperID">helperID.</param>
        /// <param name="agencyID">agencyID</param>
        /// <param name="roles">roles</param>
        /// <returns>GetPersonsListByHelperID.</returns>
        GetPersonListDTO GetPersonsListByHelperID(PersonSearchDTO personSearchDTO);

        /// <summary>
        /// GetPersonInitials
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>PersonInitialsResponseDTO</returns>
        PersonInitialsResponseDTO GetPersonInitials(Guid personIndex);

        /// <summary>
        /// Get the list of collaborations assigned to a person
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>PersonCollaborationResponseDTO</returns>
        PersonCollaborationResponseDTO GetPeopleCollaborationList(Guid personIndex, UserTokenDetails userTokenDetails, int questionnaireID = 0);

        /// <summary>
        /// GetPersonDetails.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonDetailsResponseDTO.</returns>
        PersonDetailsResponseDTO GetPersonDetails(Guid personIndex, long loggedInAgencyID);

        /// <summary>
        /// GetPeopleCollaborationListForReport
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="personQuestionaireID"></param>
        /// <param name="voiceTypeID"></param>
        /// <returns></returns>
        PersonCollaborationResponseDTO GetPeopleCollaborationListForReport(Guid personIndex, long personQuestionaireID, int voiceTypeID, UserTokenDetails userTokenDetails);

        /// <summary>
        /// GetPastNotificationList.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="notificationLogSearchDTO">notificationLogSearchDTO.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        NotificationLogResponseDTO GetPastNotifications(Guid personIndex, NotificationLogSearchDTO notificationLogSearchDTO);

        /// <summary>
        /// GetPresentNotifications.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="notificationLogSearchDTO">notificationLogSearchDTO.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        NotificationLogResponseDTO GetPresentNotifications(Guid personIndex, NotificationLogSearchDTO notificationLogSearchDTO);

        /// <summary>
        /// GetPersonSharingDetails-FEtch Sharing Details on PersonCLick
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <param name="agencyID"></param>
        /// <returns>PersonSharedDetailsResponseDTO</returns>
        PersonSharedDetailsResponseDTO GetPersonSharingDetails(Guid peopleIndex, long agencyID);

        /// <summary>
        /// RemovePersonQuestionnaire.
        /// </summary>
        /// <param name="personQuestionnaireID">person Questionnaire ID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO RemovePersonQuestionnaire(Guid personIndex, int updateUserID, int questionnaireId, long loggedInAgencyID);

        /// <summary>
        /// UpsertPerson.
        /// </summary>
        /// <param name="peopleDetailsDTO">List of peopleDetailsDTO. </param>
        /// <returns>AddPersonResponseDTO.</returns>
        CRUDResponseDTO UpsertPerson(List<PeopleDetailsDTO> peopleDetailsDTO, bool isClosed);

        AddPersonResponseDTO AddBulkPeopleDetails(List<PeopleDetailsDTO> peopleDetailsDTO, List<string> universalIDlist, long agencyID);

        /// <summary>
        /// GetPersonQuestionnaireById.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>PersonQuestionnaireDetailsDTo.</returns>
        PersonQuestionnaireDetailsDTo GetPersonQuestionnaireById(int personQuestionnaireId);

        /// <summary>
        /// GetPersonQuestionaireByPersonQuestionaireID.
        /// </summary>
        /// <param name="personQuestionnaireId">personQuestionnaireId.</param>
        /// <returns>PersonQuestionnaireListDetailsDTO.</returns>
        PersonQuestionnaireListDetailsDTO GetPersonQuestionaireByPersonQuestionaireID(int personQuestionnaireId);

        /// <summary>
        /// GetPersonCollaborationByPersonIdAndCollaborationId.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="collaborationId">collaborationId.</param>
        /// <returns>PersonCollaborationDetailsDTO.</returns>
        PersonCollaborationDetailsDTO GetPersonCollaborationByPersonIdAndCollaborationId(long personId, int? collaborationId);

        /// <summary>
        /// GetPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <param name="questionnaireWindowId">QuestionnaireWindowId.</param>
        /// <returns>PersonQuestionnaireScheduleDetailsDTO.</returns>
        PersonQuestionnaireScheduleDetailsDTO GetPersonQuestionnaireSchedule(long personQuestionnaireID, int questionnaireWindowId);

        /// <summary>
        /// UpdateBulkPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireSchedules">personQuestionnaireSchedules.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireScheduleDTO> personQuestionnaireSchedules);

        /// <summary>
        /// AddPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireSchedules">personQuestionnaireSchedules.</param>
        /// <returns>AddPersonQuestionnaireScheduleResponseDTO.</returns>
        AddPersonQuestionnaireScheduleResponseDTO AddPersonQuestionnaireSchedule(PersonQuestionnaireScheduleDTO personQuestionnaireSchedules);

        /// <summary>
        /// GetPersonQuestionnaireScheduleForReminder.
        /// </summary>
        /// <param name="personQuestionnaireSchedule">personQuestionnaireSchedule.</param>
        /// <returns>PersonQuestionnaireScheduleDetailsDTO.</returns>
        PersonQuestionnaireScheduleDetailsDTO GetPersonQuestionnaireScheduleForReminder(PersonQuestionnaireScheduleInputDTO personQuestionnaireSchedule);

        /// <summary>
        /// ImportPerson.
        /// </summary>
        /// <param name="peopleDetailsDTO"></param>
        /// <returns></returns>
        CRUDResponseDTO ImportPerson(List<PeopleDetailsDTO> peopleDetailsDTO);

        /// <summary>
        /// GetPersonsAndHelpersByPersonIDListForAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>PersonHelperEmailDetailDTO.</returns>
        PersonHelperEmailDetailDTO GetPersonsAndHelpersByPersonIDListForAlert(long personId);
        /// <summary>
        /// GetPersonsAndHelpersByPersonIDListForAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>PersonHelperEmailDetailDTO.</returns>
        PersonHelperEmailDetailDTO GetPersonsAndHelpersByPersonIDList(List<long> personId);

        /// <summary>
        /// GetDetailsByPersonQuestionScheduleList.
        /// </summary>
        /// <param name="personScheduleId">personScheduleId.</param>
        /// <returns>ReminderNotificationScheduleResponse.</returns>
        ReminderNotificationScheduleResponse GetDetailsByPersonQuestionScheduleList(List<long> personScheduleId);
        /// <summary>
        /// GetActivePersons.
        /// </summary>
        /// <returns>ActivePersonResponseDTO.</returns>
        ActivePersonResponseDTO GetActivePersons();
        /// <summary>
        /// GetActivePersonCollaboration.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>ActivePersonResponseDTO.</returns>
        ActivePersonResponseDTO GetActivePersonCollaboration(List<long> personId);

        /// <summary>
        /// UpdateIsActiveForPerson.
        /// </summary>
        /// <param name="personIds">personIds.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateIsActiveForPerson(List<long> personIds);

        /// <summary>
        /// GetNotifyReminderScheduledForToday.
        /// </summary>
        /// <returns>ReminderNotificationScheduleResponse.</returns>
        ReminderNotificationScheduleResponse GetNotifyReminderScheduledForToday(DateTime lastRunTime, DateTime currentRunTime);

        /// <summary>
        /// GetNotifyReminderScheduledCountForToday.
        /// </summary>
        /// <returns>ReminderNotificationScheduleResponse.</returns>
        ReminderNotificationScheduleResponse GetNotifyReminderScheduledCountForToday(DateTime lastRunTime, DateTime currentRunTime);


        /// <summary>
        /// GetVoiceTypeRelatedDetailsOfPerson.
        /// </summary>
        /// <param name="personstoUpload"></param>
        /// <returns></returns>
        EHRLookupResponseDTO GetVoiceTypeRelatedDetailsOfPerson(PersonIndexToUploadDTO personstoUpload);

        /// <summary>
        /// GetLeadHistoryDetails.
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        AuditPersonProfileResponseDTO GetAuditPersonProfileDetails(Guid peopleIndex, long agencyID, string historyType);

        /// <summary>
        /// GetPeopleDetailsListForExternal.
        /// </summary>
        /// <param name="personSearchInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        PeopleDetailsResponseDTOForExternal GetPeopleDetailsListForExternal(PersonSearchInputDTO personSearchInputDTO, LoggedInUserDTO loggedInUserDTO);

        /// <summary>
        /// UpdatePersonForExternal.
        /// </summary>
        /// <param name="personEditInputDTO">personEditInputDTO.</param>
        /// <returns>CRUDResponseDTOForExternalPersoneEdit.</returns>
        CRUDResponseDTOForExternalPersoneEdit UpdatePersonForExternal(PeopleEditDetailsForExternalDTO personEditInputDTO, LoggedInUserDTO loggedInUserDTO);

        /// <summary>
        /// Add person for external.
        /// </summary>
        /// <param name="personAddInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        CRUDResponseDTOForExternalPersoneAdd AddPersonForExternal(PeopleAddDetailsForExternalDTO personAddInputDTO, LoggedInUserDTO loggedInUserDTO);
        /// <summary>
        /// StopSMSInvitationSending.
        /// </summary>
        /// <param name="twilioRequest">twilioRequest</param>
        /// <returns></returns>
        string StopSMSInvitationSending(TwilioRequest twilioRequest);

        /// <summary>
        /// Get all person questionnaire schedules
        /// </summary>
        /// <returns></returns>
        PersonQuestionnairesRegularScheduleResponse GetAllPersonQuestionnairesRegularSchedule(List<long> lst_PersonQuestionnaireIds);
        /// <summary>
        /// AddBulkPersonQuestionnaireSchedule
        /// </summary>
        /// <param name="personQuestionnaireSchedules"></param>
        /// <returns></returns>
        AddBulkPersonQuestionnaireScheduleResponseDTO AddBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireScheduleDTO> personQuestionnaireSchedules);
        /// <summary>
        /// SavePersonHelperDetailsForExternal
        /// </summary>
        /// <param name="personHelperDetailsDTO"></param>
        /// <param name="updateUserID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        AddPersonHelperResponseDTOForExternal SavePersonHelperDetailsForExternal(PersonHelperAddDTOForExternal personHelperDetailsDTO, int updateUserID, long loggedInAgencyID);
        /// <summary>
        /// ExpirePersonHelperDetailsForExternal
        /// </summary>
        /// <param name="expirePersonHelperDetailsDTO"></param>
        /// <returns></returns>
        ExpirePersonHelperResponseDTOForExternal ExpirePersonHelperDetailsForExternal(ExpirePersonHelperAddDTOForExternal expirePersonHelperDetailsDTO);
        /// <summary>
        /// ExpirePersonCollaborationDetailsForExternal
        /// </summary>
        /// <param name="expirePersonCollaborationDetailsDTO"></param>
        /// <returns></returns>
        ExpirePersonCollaborationResponseDTOForExternal ExpirePersonCollaborationDetailsForExternal(ExpirePersonCollaborationAddDTOForExternal expirePersonCollaborationDetailsDTO);
        /// <summary>
        /// SavePersonCollaborationDetailsForExternal
        /// </summary>
        /// <param name="PersonCollaborationDetailsDTO"></param>
        /// <param name="updateUserID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        AddPersonCollaborationResponseDTOForExternal SavePersonCollaborationDetailsForExternal(PersonCollaborationAddDTOForExternal PersonCollaborationDetailsDTO, int updateUserID, long loggedInAgencyID);
        PersonCollaborationResponseDTO GetAllPersonCollaborationForReminders(List<long> list_personCollaborationIds);

        /// <summary>
        /// AddPersonSupportDetailsForExternal.
        /// </summary>
        /// <param name="addPersonSupportInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        CRUDResponseDTOForExternalPersonSupport AddPersonSupportDetailsForExternal(AddPersonSupportDTOForExternal addPersonSupportInputDTO, LoggedInUserDTO loggedInUserDTO);
        /// <summary>
        /// EditPersonSupportDetailsForExternal.
        /// </summary>
        /// <param name="editPersonSupportInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        CRUDResponseDTOForExternalPersonSupport EditPersonSupportDetailsForExternal(EditPersonSupportDTOForExternal editPersonSupportInputDTO, LoggedInUserDTO loggedInUserDTO);
        /// <summary>
        /// ExpirePersonSupportForExternal.
        /// </summary>
        /// <param name="expirePersonSupportInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        CRUDResponseDTOForExternalPersonSupport ExpirePersonSupportForExternal(ExpirePersonSupportDTOForExternal expirePersonSupportInputDTO, LoggedInUserDTO loggedInUserDTO);
        PersonQuestionnairesRegularScheduleResponse GetAllPersonQuestionnairesToBeScheduled();
        /// <summary>
        /// GetPeopleSupportListForExternal.
        /// </summary>
        /// <param name="personSearchInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns></returns>
        PersonSupportResponseDTOForExternal GetPeopleSupportListForExternal(PersonSupportSearchInputDTO personSearchInputDTO, LoggedInUserDTO loggedInUserDTO);
    }
}
