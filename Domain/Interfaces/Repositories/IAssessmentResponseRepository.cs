using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAssessmentResponseRepository
    {
        /// <summary>
        /// GetAssessmentValues.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">Id of a Questionnaire.</param>
        /// <param name="assessmentIDs">Id's of Assesments</param>
        /// <returns>AssessmentResponseDTO.</returns>
        List<AssessmentValuesDTO> GetAssessmentValues(Guid personIndex, int questionnaireId, string sharedAssessmentIDs, long agencyID, string helpersAssessmentIDs, string assessmentIDs);

        /// <summary>
        /// GetNeedforFocusValues.
        /// </summary>
        /// <param name="assessmentResponseIds">assessmentResponseIds.</param>
        /// <returns></returns>
        List<AssessmentValuesDTO> GetNeedforFocusValues(List<string> assessmentResponseIds);

        /// <summary>
        /// GetAssessmentValuesByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>AssessmentValuesDTO.</returns>
        List<AssessmentValuesDTO> GetAssessmentValuesByAssessmentID(int assessmentID);

        /// <summary>
        /// AddAssessmentResponse
        /// </summary>
        /// <param name="assessmentResponse"></param>
        /// <returns>AssessmentResponse</returns>
        AssessmentResponse AddAssessmentResponse(AssessmentResponse assessmentResponse);

        /// <summary>
        /// To get AssessmentResponse details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AssessmentResponse.</returns>
        Task<IReadOnlyList<AssessmentResponsesDTO>> GetAssessmentResponse(int id);

        /// <summary>
        /// To get AssessmentResponse details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AssessmentResponse.</returns>
        Task<IReadOnlyList<AssessmentResponsesDTO>> GetAllAssessmentResponses(int id);

        /// <summary>
        /// To Update AssessmentResponse.
        /// </summary>
        /// <param name="assessmentResponse">assessmentResponse.</param>
        /// <returns>AssessmentResponse.</returns>
        AssessmentResponse UpdateAssessmentResponse(AssessmentResponse assessmentResponse, bool isTracked = true);

        /// <summary>
        /// GetAssessmentResponses.
        /// </summary>
        /// <param name="AssessmentResponseID">AssessmentResponseID.</param>
        /// <returns>AssessmentResponse.</returns>
        Task<AssessmentResponse> GetAssessmentResponses(int AssessmentResponseID);

        /// <summary>
        /// AssessmentPriority.
        /// </summary>
        /// <param name="assessmentResponse">assessmentResponse.</param>
        /// <returns>AssessmentResponse List.</returns>
        List<AssessmentResponse> UpdateBulkAssessmentResponse(List<AssessmentResponse> assessmentResponse);
        List<AssessmentResponse> UpdatePriority(List<AssessmentResponse> assessmentResponse);


        List<AssessmentResponse> AddBulkAssessmentResponse(List<AssessmentResponse> assessmentResponse);
        Task<AssessmentResponse> GetAssessmentResponseByGUID(Guid AssessmentResponseGuid);
        Task<IReadOnlyList<AssessmentResponse>> GetAssessmentResponseList(List<int> AssessmentResponseIDList);
        Task<IReadOnlyList<AssessmentResponse>> GetAssessmentResponseListByGUID(List<Guid> AssessmentResponseGUIDList);
        /// <summary>
        /// GetAssessmentResponseList.
        /// </summary>
        /// <param name="AssessmentId">AssessmentId.</param>
        /// <returns>AssessmentResponse.</returns>
        Task<IReadOnlyList<AssessmentResponse>> GetAssessmentResponseListByAssessmentId(int AssessmentId);

        List<AssessmentResponsesDTO> GetAssessmentResponseFOrDashboardCalculation(long personId,int AssessmentId,int submittedStatusID,int approvedStatusID,int returnedStatusID);
        List<AssessmentResponsesDTO> GetConfidentialQuestionnaireItemID(long personID, int questionnaireID, int assessmentID);
        List<AssessmentResponse> GetAssessmentResponsesByID(List<int> QuestionItems);
        List<AssessmentResponsesDTO> GetAssessmentResponseForDefualtResponseValue(int questionnaireID, Guid personIndex, List<int> questionnaireItemID);

        /// <summary>
        /// Function to fetch the child response aassesments based on the parent id
        /// </summary>
        /// <param name="AssessmentResponseId"></param>
        /// <returns></returns>
        Task<List<AssessmentResponse>> GetAssessmentResponseListByParentId(int AssessmentResponseId);
    }
}
