// -----------------------------------------------------------------------
// <copyright file="IPersonRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IPersonRepository.
    /// </summary>
    public interface IPersonRepository : IAsyncRepository<Person>
    {


        List<PersonDTO> GetPersons(int pageNumber, int pageSize);

        int GetPersonsCount();

        /// <summary>
        /// GetPeopleDetails.
        /// </summary>
        /// <param name="peopleIndex"></param>
        /// <returns></returns>
        PeopleDataDTO GetPeopleDetails(Guid peopleIndex);

        /// <summary>
        /// To add person details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>id.</returns>
        PeopleDTO AddPerson(PeopleDTO peopleDTO);

        /// <summary>
        /// GetPeopleIdentifierList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>PeopleIdentifierDTO</returns>
        List<PeopleIdentifierDTO> GetPeopleIdentifierList(Int64 personID);

        /// <summary>
        /// GetPeopleRaceEthnicityList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>PeopleRaceEthnicityDTO</returns>
        List<PeopleRaceEthnicityDTO> GetPeopleRaceEthnicityList(Int64 personID);

        /// <summary>
        /// GetPeopleSupportList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>PeopleSupportDTO</returns>
        List<PeopleSupportDTO> GetPeopleSupportList(Int64 personID);

        /// <summary>
        /// GetPeopleHelperList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>PeopleHelperDTO</returns>
        List<PeopleHelperDTO> GetPeopleHelperList(Int64 personID);

        /// <summary>
        /// GetPeopleCollaborationList
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>PeopleCollaborationDTO</returns>
        List<PeopleCollaborationDTO> GetPeopleCollaborationList(Int64 personID, long agencyID, int questionnaireID = 0, int userID = 0);

        //List<PeopleIdentifierDTO> GetPeopleIdentifierList(long personID);
        //List<PeopleRaceEthnicityDTO> GetPeopleRaceEthnicityList(long personID);
        /// <summary>
        /// To update person details.
        /// </summary>
        /// <param name="peopleDTO">id.</param>
        /// <returns>List of summaries.</returns>
        PeopleDTO UpdatePerson(PeopleDTO peopleDTO);
        /// <summary>
        /// To get details Person.
        /// </summary>
        /// <param id="id">id.</param>
        /// <returns>.PeopleDTO</returns>
        PeopleDTO GetPerson(Guid id);

        /// <summary>
        /// GetPersonByLanguageCount
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns>PeopleDTO</returns>
        int GetPersonByLanguageCount(int languageID);

        /// <summary>
        /// GetRiskNotificationList.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <returns>RiskNotificationsListDTO List.</returns>
        List<RiskNotificationsListDTO> GetRiskNotificationList(long personID);

        /// <summary>
        /// GetReminderNotificationList.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <returns>ReminderNotificationsListDTO List.</returns>
        List<ReminderNotificationsListDTO> GetReminderNotificationList(long personID);

        /// <summary>
        /// GetPastNotificationList.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <returns>PastNotificationsListDTO List.</returns>
        List<PastNotificationsListDTO> GetPastNotificationList(long personID);

        /// <summary>
        /// GetAllNotes.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PastNotesResponseDTO.</returns>
        List<NotificationNotesDTO> GetAllPastNotes(int notificationLogID, int pageNumber, int pageSize);

        /// <summary>
        /// GetPersonsListByHelperID
        /// </summary>
        /// <param name="personSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="callingType">can be list/count.If count shared persons will not be included</param>
        /// <returns></returns>
        List<PersonDTO> GetPersonsListByHelperID(PersonSearchDTO personSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);

        /// <summary>
        /// GetPersonsListByHelperIDCount.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID">agencyID</param>
        /// <param name="helperID">helperID</param>
        /// <returns></returns>
       Tuple<int,int> GetPersonsListByHelperIDCount(PersonSearchDTO personSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);

        /// <summary>
        /// GetPersonInitials
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns>PersonInitialsDTO</returns>
        PersonInitialsDTO GetPersonInitials(Guid personIndex);

        /// <summary>
        /// GetPersonDetails.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <returns>PersonDetailsDTO.</returns>
        PersonDetailsDTO GetPersonDetails(Guid personIndex);

        /// <summary>
        /// GetPeopleCollaborationListForReport
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="personQuestionaireID"></param>
        /// <param name="voiceTypeID"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        List<PeopleCollaborationDTO> GetPeopleCollaborationListForReport(Int64 personID, long personQuestionaireID, int voiceTypeID, UserTokenDetails userTokenDetails);

        /// <summary>
        /// GetSharedAssessmentIDs
        /// </summary>
        /// <param name="sharedPersonSearchDTO"></param>
        /// <returns></returns>
        string GetSharedAssessmentIDs(SharedPersonSearchDTO sharedPersonSearchDTO);
        /// <summary>
        /// GetSharedPersonQuestionnaireDetails
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        SharedDetailsDTO GetSharedPersonQuestionnaireDetails(long personID, long loggedInAgencyID);
        /// <summary>
        /// isSharedPerson
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        public bool IsSharedPerson(long personID, long loggedInAgencyID);
        List<PersonSharingDetailsDTO> GetPersonSharingDetails(Guid personIndex, long loggedInAgencyID);

        /// <summary>
        /// GetSharedPersonIDs 
        /// </summary>
        /// <param name="agencyID"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        string GetSharedPersonIDs(long agencyID, string role);

        /// <summary>
        /// get active persons
        /// </summary>
        /// <returns></returns>
        List<long> GetActivePersons();

        /// <summary>
        /// get active person collaborations
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        List<long> GetActivePersonsCollaboration(List<long> personID);

        /// <summary>
        /// bulk update persons
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        List<Person> UpdateBulkPersons(List<Person> persons);

        /// <summary>
        /// GetPersonByAssessmentID
        /// </summary>
        /// <param name="assessmentID">assessmentID</param>
        /// <returns>Person</returns>
        PersonQuestionnaireDetailsDTO GetPersonByAssessmentID(int assessmentID);

        /// <summary>
        /// getPersonByUniversalId
        /// </summary>
        /// <param name="UniveralId">UniveralId</param>
        /// <returns></returns>
        PeopleDataDTO getPersonByUniversalId(string UniveralId);

        List<PeopleDataDTO> getPersonByUniversalIdList(List<string> universalIdList, long agencyId);
        int GetUniversalIDCountByAgency(long agencyID);
        PersonInitialsDTO GetPersonInitialByPersonID(long personIID);
        List<PeopleHelperEmailDTO> getPersonsAndHelpersByPersonIDList(List<long> personIdList);
        List<PeopleHelperEmailDTO> getPersonsAndHelpersByPersonIDListForAlert(long personIdList);
        List<PersonQuestionnaireScheduleEmailDTO> getDetailsByPersonQuestionScheduleList(List<long> lstpersonID);
        List<Person> ImportPersonBulkInsert(List<Person> personList);
        /// <summary>
        /// GetPersonListByGUID.
        /// </summary>
        /// <param name="personIndexGuids">personIndexGuids.</param>
        /// <returns>PersonList.</returns>
        Task<IReadOnlyList<Person>> GetPersonListByGUID(List<Guid> personIndexGuids);

        /// <summary>
        /// GetPersonsDetailsListForExternal.
        /// </summary>
        /// <param name="loggedInUserDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns></returns>
        List<PeopleProfileDetailsDTO> GetPersonsDetailsListForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO);
        /// <summary>
        /// IsValidPersonInAgency.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="personAgencyID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        bool IsValidPersonInAgency(long personID, long personAgencyID, long loggedInAgencyID);
        /// <summary>
        /// IsValidPersonInAgency.
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        bool IsValidPersonInAgency(Guid personIndex, long loggedInAgencyID);
        /// <summary>
        /// IsValidPersonInAgencyForQuestionnaire.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="personAgencyID"></param>
        /// <param name="questionnaireID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <param name="isEmailAssessment"></param>
        /// <returns></returns>
        bool IsValidPersonInAgencyForQuestionnaire(long personID, long personAgencyID, int questionnaireID, long loggedInAgencyID, bool isEmailAssessment = false);
        /// <summary>
        /// IsValidPersonInAgencyForQuestionnaire.
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <param name="isEmailAssessment"></param>
        /// <returns></returns>
        bool IsValidPersonInAgencyForQuestionnaire(Guid personIndex, int questionnaireID, long loggedInAgencyID, bool isEmailAssessment = false);
        /// <summary>
        /// GetPersonHelperCollaborationDetails.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        SharedDetailsDTO GetPersonHelperCollaborationDetails(long personID, long loggedInAgencyID, int userID);
        /// <summary>
        /// GetHelpersAssessmentIDs.
        /// </summary>
        /// <param name="sharedPersonSearchDTO"></param>
        /// <returns></returns>
        SharedDetailsDTO GetHelpersAssessmentIDs(SharedPersonSearchDTO sharedPersonSearchDTO);
        /// <summary>
        /// GetPersonByPersonId
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        PeopleDTO GetPersonByPersonId(long personId, long agencyId);
        /// <summary>
        /// GetPersonalDetails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PeopleDTO GetPersonalDetails(Guid id);

        /// <summary>
        /// GetPeopleSupportListForExternal
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        List<PeopleSupportExternalDTO> GetPeopleSupportForExternalByPersonId(long personID);
        /// <summary>
        /// GetPeopleHelperListForExternal
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        List<PeopleHelperExternalDTO> GetPeopleHelperForExternalByPersonId(long personID);
        /// <summary>
        /// GetPeopleCollaborationListForExternal
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        List<PeopleCollaborationExternalDTO> GetPeopleCollaborationForExternalByPersonId(long personID);
    }
}
