using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IPersonAssessmentMetricsRepository
    {
        /// <summary>
        /// GetPersonAssessmentMetricsInDetail
        /// </summary>
        /// <param name="metricsInput"></param>
        /// <returns></returns>
        List<PersonAssessmentMetrics> GetPersonAssessmentMetricsInDetail(DashboardMetricsInputDTO metricsInput);
        /// <summary>
        /// UpdateBulkPersonAssessmentMetrics.
        /// </summary>
        /// <param name="personAssessmentMetrics"></param>
        /// <returns></returns>
        List<PersonAssessmentMetrics> UpdateBulkPersonAssessmentMetrics(List<PersonAssessmentMetrics> personAssessmentMetrics);
        /// <summary>
        /// AddBulkPersonAssessmentMetrics.
        /// </summary>
        /// <param name="personAssessmentMetrics"></param>
        /// <returns></returns>
        List<PersonAssessmentMetrics> AddBulkPersonAssessmentMetrics(List<PersonAssessmentMetrics> personAssessmentMetrics);
    }
}
