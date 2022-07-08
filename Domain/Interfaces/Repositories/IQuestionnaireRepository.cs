using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireRepository
    {
        /// <summary>
        /// GetQuestionnaireList.
        /// </summary>
        /// <param name="QuestionnaireSearchDTO">questionnaireSearchDTO</param>
        /// <param name="DynamicQueryBuilderDTO">queryBuilderDTO</param>
        /// <returns>QuestionnaireDTO</returns>
        List<QuestionnaireDTO> GetQuestionnaireList(QuestionnaireSearchDTO questionnaireSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);

        /// <summary>
        /// GetQuestionnaireCount.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="from"></param>
        /// <param name="TenentID"></param>
        /// <returns></returns>
        int GetQuestionnaireCount(long tenantID);
        /// <summary>
        /// To update Questionnaire.
        /// </summary>
        /// <param name="questionnaireDTO">id.</param>
        /// <returns>List of summaries.</returns>
        QuestionnairesDTO UpdateQuestionnaire(QuestionnairesDTO questionnaireDTO);


        /// <summary>
        /// To get details Questionnaire.
        /// </summary>
        /// <param QuestionnaireDTO="QuestionnaireDTO">id.</param>
        /// <returns>.QuestionnaireDTO</returns>       

        Task<QuestionnairesDTO> GetQuestionnaire(int id);

        /// <summary>
        /// To Get QuestionnaireWindow.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireWindowDTO.</returns>
        List<QuestionnaireWindowDTO> GetQuestionnaireWindow(int questionnaireID);

        /// <summary>
        /// To Get QuestionnaireReminderRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireReminderRuleDTO.</returns>
        List<QuestionnaireReminderRuleDTO> GetQuestionnaireReminderRule(int questionnaireID);

        /// <summary>
        /// GetQuestions.
        /// </summary>
        /// <param name="id">Questionnaire ID</param>
        /// <returns>QuestionsResponseDTO.</returns>
        QuestionsDTO GetQuestions(int id);

        /// <summary>
        /// GetPersonQuestionnaireList
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>PersonQuestionnaireListDTO</returns>
        List<PersonQuestionnaireListDTO> GetPersonQuestionnaireList(Guid personIndex, int? questionnaireID, int pageNumber, int pageSize, string sharedQuestionIds, string helperColabQuestionIDs);

        /// <summary>
        /// GetPersonQuestionnaireCount
        /// </summary>
        /// <returns>int</returns>
        int GetPersonQuestionnaireCount(Guid personIndex);

        /// <summary>
        /// GetAllQuestionnairesWithAgency.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PersonQuestionnaireDataDTO list.</returns>
        List<PersonQuestionnaireDataDTO> GetAllQuestionnairesWithAgency(long agencyID);

        /// CloneQuestionnaire
        /// </summary>
        /// <param name="questionnaireDTO"></param>
        /// <returns>QuestionnairesDTO</returns>
        QuestionnairesDTO CloneQuestionnaire(QuestionnairesDTO questionnaireDTO);

        /// <summary>
        /// GetAllQuestionnaireWithCompletedAssessment For Reports.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personID">personID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="collaborationID">collaborationID.</param>
        /// <param name="voicetypeID">voicetypeID.</param>
        /// <returns>Tuple<List<AssessmentQuestionnaireDataDTO>,int>.</returns>
        Tuple<List<AssessmentQuestionnaireDataDTO>, int> GetAllQuestionnaireWithCompletedAssessment(long agencyID, long personID, int pageNumber, int pageSize, long personCollaborationID, int voicetypeID, long voiceTypeFKID, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs);

        /// <summary>
        /// GetAllQuestionnaireWithCompletedAssessmentCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personID">personID.</param>
        /// <returns>Count.</returns>
        int GetAllQuestionnaireWithCompletedAssessmentCount(long agencyID, long personID);

        /// <summary>
        /// GetAllQuestionnairesWithAgencyCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>QuestionnaireDataDTO Count.</returns>
        int GetAllQuestionnairesWithAgencyCount(long agencyID);

        /// <summary>
        /// QuestionnaireByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>QuestionnaireID</returns>
        int QuestionnaireByAssessmentID(int assessmentID);

        /// <summary>
        /// Get reference count for a questionnaire.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>int.</returns>
        int GetQuestionnaireUsedCountByID(int questionnaireID);

        /// <summary>
        /// GetAllQuestionnaireItemsWithResponses
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns></returns>
        List<QuestionnaireItemsForImportDTO> GetAllQuestionnaireItemsWithResponses(int questionnaireID);

        /// <summary>
        /// GetQuestionnaireDetailsbyIds.
        /// </summary>
        /// <param name="questionnaireIds">questionnaireIds.</param>
        /// <returns>QuestionnairesDTO.</returns>
        List<QuestionnairesDTO> GetQuestionnaireDetailsbyIds(List<int> questionnaireIds);

        /// <summary>
        /// GetQuestionDetails.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        QuestionsDTO GetQuestionDetails(int id);

        /// <summary>
        /// GetQuestionsSkippedActionDetails.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        QuestionnaireSkipActionDetailsDTO GetQuestionsSkippedActionDetails(int id);
    }
}
