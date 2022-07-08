using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAssessmentRepository 
    {

        /// <summary>
        /// GetAssessmentDetails.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">ID of a Questionnaire.</param>
        /// <param name="date">date.</param> 
        /// <param name="helpersAssessmentIDs">helpersAssessmentIDs.</param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>AssessmentDetailsResponseDTO.</returns>
        List<AssessmentDetailsDTO> GetAssessmentDetails(Guid personIndex, int questionnaireId, DateTime? date, string sharedAssessmentIDs, long agencyID, string helpersAssessmentIDs, int pageNumber, int pageSize);

        /// <summary>
        /// AddAssessment
        /// </summary>
        /// <param name="assessment"></param>
        /// <returns>Assessment</returns>
        Assessment AddAssessment(Assessment assessment);

        /// <summary>
        /// To get Assessment details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>CollaborationDTO.</returns>
        Task<Assessment> GetAssessment(int id);

        /// <summary>
        /// To Update Assessment.
        /// </summary>
        /// <param name="assessment">assessment.</param>
        /// <returns>List of summaries.</returns>
        Assessment UpdateAssessment(Assessment assessment);

        /// <summary>
        /// GetPersonQuestionnaireID
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>long</returns>
        long GetPersonQuestionnaireID(Guid personIndex, int questionnaireID);

        /// <summary>
        /// GetPersonIdFromAssessment
        /// </summary>
        /// <param name="assessmentId"></param>
        /// <returns>long PersonId</returns>
        long GetPersonIdFromAssessment(int assessmentId);

        /// <summary>
        /// Get Last Assessment ByPerson
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns></returns>
        List<Assessment> GetLastAssessmentByPerson(long personID, long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID, SharedDetailsDTO sharedDTO,long agencyID, SharedDetailsDTO helperColbDTO, int rowCount = 1);

        Assessment GetAssessmentByPersonQuestionaireID(long personQuestionnaireID, int assessmentStatusID);

        /// <summary>
        /// GetQuestionDetailsFromAssessment ID.
        /// </summary>
        /// <param name="assessmentId">assessmentId.</param>
        /// <returns>Questionnaire object.</returns>
        Questionnaire GetQuestionDetailsFromAssessment(int assessmentId);

        /// <summary>
        /// AddBulkAssessments.
        /// </summary>
        /// <param name="assessments"></param>
        /// <returns></returns>
        List<Assessment> AddBulkAssessments(List<Assessment> assessments);

        /// <summary>
        /// GetAssessmentListByGUID.
        /// </summary>
        /// <param name="AssessmentResponseIDList"></param>
        /// <returns></returns>
        List<Assessment> GetAssessmentListByGUID(List<Guid?> assessmentGUIDList);
        /// <summary>
        /// GetAssessmentDetailsForEHRUpdate.
        /// </summary>
        /// <param name="approvedStatusID"></param>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        List<EHRAssessmentDTO> GetAssessmentDetailsForEHRUpdate(string ehrAssessmentIDs);
        /// <summary>
        /// GetAssessmentIDsForEHRUpdate.
        /// </summary>
        /// <param name="approvedStatusID"></param>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        List<string> GetAssessmentIDsForEHRUpdate(int approvedStatusID, long agencyId, string EHRUpdateStatus);
        /// <summary>
        /// GetAssessmentListByID.
        /// </summary>
        /// <param name="assessmentIDList">assessmentIDList.</param>
        /// <returns>Assessment.</returns>
        List<Assessment> GetAssessmentListByID(List<int> assessmentIDList);
        /// <summary>
        /// UpdateBulkAssessments.
        /// </summary>
        /// <param name="assessments"></param>
        /// <returns></returns>
        List<Assessment> UpdateBulkAssessments(List<Assessment> assessments);
        /// <summary>
        /// GetAssessmentDetailsListsForExternal.
        /// </summary>
        /// <param name="loggedInUserDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns></returns>
        List<AssessmentDetailsListDTO> GetAssessmentDetailsListsForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO, Guid? helperIndex);
        List<InviteReceiversInDetailDTO> GetReceiversDetailsForReminderInvite(List<long> personQuestionnaireIds, string typeOfInvite);

        /// <summary>
        /// GetAssessmentDetails.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">ID of a Questionnaire.</param>
        /// <param name="date">date.</param> 
        /// <param name="helpersAssessmentIDs">helpersAssessmentIDs.</param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>AssessmentDetailsResponseDTO.</returns>
        AssessmentDetailsDTO GetAssessmentPageNumberByAssessmentID(Guid personIndex, int questionnaireId, int assessmentId, long agencyID);

    }
}
