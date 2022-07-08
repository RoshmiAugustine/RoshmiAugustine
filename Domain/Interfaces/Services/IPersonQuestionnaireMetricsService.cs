// -----------------------------------------------------------------------
// <copyright file="IPersonQuestionnaireMetricsService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------


using System.Collections.Generic;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// IPersonQuestionnaireMetricsService.
    /// </summary>
    public interface IPersonQuestionnaireMetricsService
    {
        /// <summary>
        /// Get data for Dashboard Strengths metrics
        /// </summary>
        /// <param name="strengthMetricsSearchDTO"></param>
        /// <returns>DashboardStrengthMetricsListResponseDTO</returns>
        DashboardStrengthMetricsListResponseDTO GetDashboardStrengthMetrics(StrengthMetricsSearchDTO strengthMetricsSearchDTO);

        /// <summary>
        /// Get data for Dashboard Needs metrics
        /// </summary>
        /// <param name="needMetricsSearchDTO"></param>
        /// <returns>DashboardNeedMetricsListResponseDTO</returns>
        DashboardNeedMetricsListResponseDTO GetDashboardNeedMetrics(NeedMetricsSearchDTO needMetricsSearchDTO);

        /// <summary>
        /// Get data for Dashboard Need pie chart
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns>DashboardNeedPieChartResponseDTO</returns>
        DashboardNeedPieChartResponseDTO GetDashboardNeedPiechartData(int? helperID, long agencyID, List<string> userRole, bool isSameAsLoggedInUser, int userID);

        /// <summary>
        /// Get data for Dashboard Strength pie chart
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns>DashboardStrengthPieChartResponseDTO</returns>
        DashboardStrengthPieChartResponseDTO GetDashboardStrengthPiechartData(int? helperID, long agencyID, List<string> userRole, bool isSameAsLoggedInUser, int userID);

        /// <summary>
        /// UpdatePersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personQuestionnaireMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdatePersonQuestionnaireMetrics(List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics);
        /// <summary>
        /// AddPersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personQuestionnaireMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddPersonQuestionnaireMetrics(List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics);
        /// <summary>
        /// GetPersonQuestionnaireMetrics.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="itemId">itemId.</param>
        /// <returns>PersonQuestionnaireMetricsDetailsDTO.</returns>
        PersonQuestionnaireMetricsDetailsDTO GetPersonQuestionnaireMetrics(DashboardMetricsInputDTO metricsInput);
        /// <summary>
        /// GetPersonAssessmentMetricsInDetail.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="itemId">itemId.</param>
        /// <returns>PersonQuestionnaireMetricsDetailsDTO.</returns>
        PersonAssessmentMetricsDetailsDTO GetPersonAssessmentMetricsInDetail(DashboardMetricsInputDTO metricsInput);
        /// <summary>
        /// UpdateBulkPersonAssessmentMetrics.
        /// </summary>
        /// <param name="personAssessmentMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateBulkPersonAssessmentMetrics(List<PersonAssessmentMetricsDTO> personAssessmentMetrics);
        /// <summary>
        /// AddBulkPersonAssessmentMetrics.
        /// </summary>
        /// <param name="personAssessmentMetrics">personQuestionnaireMetrics.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddBulkPersonAssessmentMetrics(List<PersonAssessmentMetricsDTO> personAssessmentMetrics);
    }
}
