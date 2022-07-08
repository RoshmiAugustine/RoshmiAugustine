using System;
using System.Collections.Generic;
using System.Text;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyInsightDashboardRepository.
    /// </summary>
    public interface IAgencyInsightDashboardRepository : IAsyncRepository<AgencyInsightDashboard>
    {
        /// <summary>
        /// GetInsightDashboardDetailsById.
        /// </summary>
        /// <param name="insightDashboardId"></param>
        /// <returns></returns>
        AgencyInsightDashboard GetInsightDashboardDetailsById(int insightDashboardId);
        /// <summary>
        /// GetInsightDashboardDetailsByAgencyId.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        List<AgencyInsightDashboard> GetInsightDashboardDetailsByAgencyId(long agencyId);
    }
}
