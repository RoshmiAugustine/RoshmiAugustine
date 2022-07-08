// -----------------------------------------------------------------------
// <copyright file="IPersonQuestionnaireRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface IPersonQuestionnaireRepository : IAsyncRepository<PersonQuestionnaire>
    {
        /// <summary>
        /// To add personQuestionnaire details.
        /// </summary>
        /// <param name="personQuestionnaireDTO"></param>
        /// <returns>Guid.</returns>
        long AddPersonQuestionnaire(PersonQuestionnaireDTO personQuestionnaireDTO);

        /// <summary>
        /// To update PersonQuestionnaire details.
        /// </summary>
        /// <param name="personQuestionnaireDTO">id.</param>
        /// <returns>List of summaries.</returns>
        PersonQuestionnaireDTO UpdatePersonQuestionnaire(PersonQuestionnaireDTO personQuestionnaireDTO);

        /// <summary>
        /// To get details PersonQuestionnaire.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        Task<PersonQuestionnaireDTO> GetPersonQuestionnaire(long id, long personID);

        /// <summary>
        /// To get details PersonQuestionnaireList.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaire</returns>
        Task<IReadOnlyList<PersonQuestionnaireDTO>> GetPersonQuestionnaireList(long id);

        /// <summary>
        /// To GetcollaborationQuestionaire.
        /// </summary>
        /// <returns> Task<PersonQuestionnaire>.</returns>
        Task<PersonQuestionnaire> GetcollaborationQuestionaire(int collaborationID, long questionnaireID);

        Task<PersonQuestionnaireDTO> GetPersonQuestionnaireByID(long personQuestionnaireID);

        Task<IReadOnlyList<PersonQuestionnaireDTO>> GetPersonQuestionnaireByCollaborationAndQuestionnaire(long collaborationId, long questionnaireId);
        List<PersonQuestionnaire> UpdateBulkPersonQuestionnaires(List<PersonQuestionnaire> personQuestionnaire);
        //List<PersonQuestionnaireDTO> BulkUpdatePersonQuestionnaires(List<PersonQuestionnaireDTO> personQuestionnaireDTOList);
        PersonQuestionnaireDTO GetPersonQuestionnaireWithNoAssessment(long id, long personID);
        List<long> GetAllPersonQuestionnaireIdsByCollaborationID(long collaborationID);
        List<long> GetAllPersonQuestionnaireIdsByQuestionnaireID(int questionnaireID);
        List<PersonQuestionnaire> GetPersonQuestionaireByPersonQuestionaireID(long personQuestionnaireID);
        List<Assessment> GetAssessmentsByPersonQuestionaireID(long personQuestionnaireID, List<int> assessmentStatusIDList);



        /// <summary>
        /// UpdatePersonQuestionnaire.
        /// </summary>
        /// <param name="personQuestionnaire">personQuestionnaire.</param>
        /// <returns>PersonQuestionnaire.</returns>
        PersonQuestionnaire UpdatePersonQuestionnaire(PersonQuestionnaire personQuestionnaire);
       

        /// <summary>
        /// GetAssessmentsByPersonIndex
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>list of Assessment</returns>
        List<Assessment> GetAssessmentsByPersonIndex(Guid personIndex, int questionnaireID);

        /// <summary>
        /// GetPersonQuestionnaireByPersonIndex
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>list of Assessment</returns>
        List<PersonQuestionnaire> GetPersonQuestionnaireByPersonIndex(Guid personIndex, int questionnaireID);
        void AddBulkPersonQuestionnaire(List<PersonQuestionnaireDTO> personQuestionnaireDTO);
        Task<PersonQuestionnaireDTO> GetPersonQuestionnaireByCollaborationQuestionnaireAndPersonID(long collaborationId, long questionnaireId, long personID);

        List<PersonQuestionnaire> GetPersonQuestionnaireWithNoAssessmentById(List<int> questionnaireId, long personID);


        /// <summary>
        /// GetAllpersonQuestionnaire.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="questionnaireID"></param>
        /// <returns></returns>
        List<PersonQuestionnaire> GetAllpersonQuestionnaire(List<long> personID, int questionnaireID);

        /// <summary>
        /// GetPersonByPersonQuestionnaireID.
        /// </summary>
        /// <param name="personQuestionnaireID"></param>
        /// <returns></returns>
        Person GetPersonByPersonQuestionnaireID(long personQuestionnaireID);
        /// <summary>
        /// GetPersonCollaborationQuestionnaireList.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="collaborationIds">Null when call from add new person</param>
        /// <returns></returns>
        List<PersonQuestionnaireDTO> GetPersonQuestionnaireIdsWithReminderOn(long personId, List<int> collaborationIds = null);
    }
}
