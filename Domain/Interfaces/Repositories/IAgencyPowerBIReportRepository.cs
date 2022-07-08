using System;
using System.Collections.Generic;
using System.Text;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAgencyPowerBIReportRepository : IAsyncRepository<AgencyPowerBIReport>
    {
        /// <summary>
        /// GetAllPowerBIReportsForAgency.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="instrumentId"></param>
        /// <returns></returns>
        List<AgencyPowerBIReport> GetAllPowerBIReportsForAgency(long agencyId, int instrumentId);
        /// <summary>
        /// GetPowerBIReportDetailsById.
        /// </summary>
        /// <param name="agencyPowerBIReportId"></param>
        /// <returns></returns>
        AgencyPowerBIReport GetPowerBIReportDetailsById(int agencyPowerBIReportId, long agencyId);
        PowerBiFilterDTO GetFilterReplaceDetailsForPowerBI(PowerBiInputDTO powerBiInputDTO);
    }
}
