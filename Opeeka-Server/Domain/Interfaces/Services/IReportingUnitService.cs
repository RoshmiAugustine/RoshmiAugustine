// -----------------------------------------------------------------------
// <copyright file="IReportingUnitService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IReportingUnitService
    {
        /// <summary>
        /// AddReportingUnit
        /// </summary>
        /// <param name="reportingUnitData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO AddReportingUnit(ReportingUnitInputDTO reportingUnitData);

        /// <summary>
        /// AddPartnerAgency
        /// </summary>
        /// <param name="partnerAgencyData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO AddPartnerAgency(PartnerAgencyInputDTO partnerAgencyData);

        /// <summary>
        /// GetPartnerAgencyList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>ReportingUnit List.</returns>
        GetPartnerAgencyListDTO GetPartnerAgencyList(int reportingUnitID, int pageNumber, int pageSize);

        /// <summary>
        /// GetReportingUnitList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ReportingUnit List.</returns>
        GetReportingUnitListDTO GetReportingUnitList(long agencyID, int pageNumber, int pageSize);

        /// <summary>
        /// GetRUCollaborationList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">collaborationID.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>Collaboration List.</returns>
        GetRUCollaborationListDTO GetRUCollaborationList(long agencyID, int reportingUnitID, int pageNumber, int pageSize);

        /// AddPartnerAgency
        /// </summary>
        /// <param name="partnerAgencyData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO UpdatePartnerAgency(PartnerAgencyInputDTO partnerAgencyData,int userID);
        /// <summary>
        /// Add ReportingUnit
        /// </summary>
        /// <param name="reportingUnitData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        CRUDResponseDTO EditReportingUnit(EditReportingUnitDTO editReportingUnitDTO);

        /// <summary>
        /// AddCollaborationSharing
        /// </summary>
        /// <param name="collaborationData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO AddCollaborationSharing(CollaborationSharingInputDTO collaborationData);

        /// <summary>
        /// EditCollaborationSharing
        /// </summary>
        /// <param name="collaborationData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO EditCollaborationSharing(CollaborationSharingInputDTO collaborationData,int userID);
    }
}
