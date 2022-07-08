using System;
using System.Collections.Generic;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IReportRepository
    {
        /// <summary>
        /// GetItemReportData.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <returns>ItemDetailsDTO List.</returns>
        List<ItemDetailsDTO> GetItemReportData(ReportInputDTO reportInputDTO, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs);
        /// <summary>
        /// GetStoryMapReportData.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <returns>StoryMapDTO List.</returns>
        List<StoryMapDTO> GetStoryMapReportData(ReportInputDTO reportInputDTO, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs);

        /// <summary>
        /// Get Person Strength Family Report .
        /// </summary>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <returns>PersonStrengthReportDTO.</returns>
        PersonStrengthReportDTO GetPersonStrengthFamilyReportData(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs);

        /// <summary>
        /// GetPersonNeedsFamilyReportData.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <param name="familyReportInputDTO">familyReportInputDTO.</param>
        /// <param name="latestAssessmentIDs">latestAssessmentIDs.</param>
        /// <returns></returns>
        PersonNeedsReportDTO GetPersonNeedsFamilyReportData(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs);
        SupportResourceReportDTO GetSupportResourcesFamilyReportData(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs);
        SupportNeedsReportDTO GetSupportNeedsFamilyReportData(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs);
        /// <summary>
        /// GetFamilyReportStatus.
        /// </summary>
        /// <param name="personID">personID</param>
        /// <param name="familyReportInputDTO">familyReportInputDTO</param>
        /// <param name="latestAssessmentIDs">latestAssessmentIDs</param>
        /// <returns></returns>
        FamilyReportStatusDTO GetFamilyReportStatus(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs);
        /// <summary>
        /// GetAllQuestionnairesForSuperStoryMap.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="sharedIDs">sharedIDs.</param>
        /// <returns></returns>
        Tuple<List<AssessmentQuestionnaireDataDTO>, int> GetAllQuestionnairesForSuperStoryMap(long personID, long personCollaborationID, int pageNumber, int pageSize, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs);
        /// <summary>
        /// GetAllDetailsForSuperStoryMap.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <param name="sharedIDs">sharedIDs.</param>
        /// <returns></returns>
        List<SuperStoryMapDTO> GetAllDetailsForSuperStoryMap(SuperStoryInputDTO reportInputDTO, SharedDetailsDTO sharedIDs);
    }
}
