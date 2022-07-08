// -----------------------------------------------------------------------
// <copyright file="IReportingUnitRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IReportingUnitRepository
    {
        /// <summary>
        /// AddReportingUnit
        /// </summary>
        /// <param name="reportingUnitDTO"></param>
        /// <returns>ReportingUnitDTO</returns>
        ReportingUnitDTO AddReportingUnit(ReportingUnitDTO reportingUnitDTO);

        /// <summary>
        /// GetPartnerAgencyList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>ReportingUnit List.</returns>
        List<PartnerAgencyDataDTO> GetPartnerAgencyList(int reportingUnitID, int pageNumber, int pageSize);

        /// GetReportingUnitList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ReportingUnit List.</returns>
        List<ReportingUnitDataDTO> GetReportingUnitList(long agencyID, int pageNumber, int pageSize);

        /// <summary>
        /// GetReportingUnitCount.
        /// </summary>
        /// <returns>Collaboration Count.</returns>
        int GetReportingUnitCount(long agencyID);

        /// <summary>
        /// GetRUCollaborationList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">collaborationID.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>Collaboration List.</returns>
        List<RUCollaborationDataDTO> GetRUCollaborationList(long agencyID, int reportingUnitID, int pageNumber, int pageSize);

        /// <summary>
        /// To update ReportingUnit details.
        /// </summary>
        /// <param name="reportingUnitDTO">id.</param>
        /// <returns>List of summaries.</returns>
        ReportingUnit UpdateReportingUnit(ReportingUnit reportingUnit);
        /// <summary>
        /// To get details ReportingUnit.
        /// </summary>
        /// <param agencyPersonCollaborationDTO="agencyPersonCollaborationDTO">id.</param>
        /// <returns>.AgencyPersonCollaborationDTO</returns>
        Task<ReportingUnit> GetReportingUnit(Guid id);
    }
}
