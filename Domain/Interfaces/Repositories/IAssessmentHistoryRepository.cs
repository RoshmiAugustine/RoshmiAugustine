using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAssessmentHistoryRepository
    {
        /// <summary>
        /// AddAssessmentHistory.
        /// </summary>
        /// <param name="assessmentHistory">assessmentHistory.</param>
        /// <returns>ReviewerHistory</returns>
        ReviewerHistory AddAssessmentHistory(ReviewerHistory assessmentHistory);

        /// <summary>
        /// GetHistoryForAssessment
        /// </summary>
        /// <param name="assessmentID">assessmentID</param>
        /// <param name="statusId">statusId</param>
        /// <returns>ReviewerHistory</returns>
        ReviewerHistory GetHistoryForAssessment(int assessmentID, int statusId);
    }
}
