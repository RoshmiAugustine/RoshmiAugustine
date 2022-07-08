// -----------------------------------------------------------------------
// <copyright file="IPersonQuestionnaireMetricsRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IPersonQuestionnaireMetricsRepository
    {
        /// <summary>
        /// Get data for Dashboard Strengths metrics
        /// </summary>
        /// <param name="strengthMetricsSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns>Tuple<List<DashboardStrengthMetricsDTO>,int></returns>
        Tuple<List<DashboardStrengthMetricsDTO>, int> GetDashboardStrengthMetrics(StrengthMetricsSearchDTO strengthMetricsSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);

        /// <summary>
        /// Get data for Dashboard Needs metrics
        /// </summary>
        /// <param name="needhMetricsSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns> Tuple<List<DashboardNeedMetricsDTO>,int></returns>
        Tuple<List<DashboardNeedMetricsDTO>, int> GetDashboardNeedMetrics(NeedMetricsSearchDTO needhMetricsSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);
        /// <summary>
        /// Get data for Dashboard Need pie chart
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns>DashboardNeedPieChartDTO</returns>
        DashboardNeedPieChartDTO GetDashboardNeedPiechartData(string role, long agencyID, int? helperID, bool isSameAsLoggedInUser, int userID);

        /// <summary>
        /// Get data for Dashboard Strength pie chart
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns>DashboardStrengthPieChartDTO</returns>
        DashboardStrengthPieChartDTO GetDashboardStrengthPiechartData(string role, long agencyID, int? helperID, bool isSameAsLoggedInUser, int userID);

        List<PersonQuestionnaireMetrics> GetPersonQuestionnaireMetrics(DashboardMetricsInputDTO metricsInput);

        PersonQuestionnaireMetrics AddPersonQuestionnaireMetrics(PersonQuestionnaireMetrics personQuestionnaireMetrics);
        PersonQuestionnaireMetrics UpdatePersonQuestionnaireMetrics(PersonQuestionnaireMetrics personQuestionnaireMetrics);

        List<PersonQuestionnaireMetrics> UpdateBulkPersonQuestionnaireMetrics(List<PersonQuestionnaireMetrics> personQuestionnaireMetrics);
        List<PersonQuestionnaireMetrics> AddBulkPersonQuestionnaireMetrics(List<PersonQuestionnaireMetrics> personQuestionnaireMetrics);


    }
}
