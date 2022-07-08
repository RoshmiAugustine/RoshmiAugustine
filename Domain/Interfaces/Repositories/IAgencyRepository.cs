// -----------------------------------------------------------------------
// <copyright file="IAgencyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface IAgencyRepository : IAsyncRepository<Agency>
    {
        /// <summary>
        /// To add agent details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List of summaries.</returns>
        long AddAgency(AgencyDTO agencyDTO);

        /// <summary>
        /// To add agent details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List of summaries.</returns>
        AgencyDTO UpdateAgency(AgencyDTO agencyDTO);


        /// <summary>
        /// To get agent details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AgencyDTO.</returns>
        Task<AgencyDTO> GetAsync(Guid id);

        /// <summary>
        /// GetAgencyList
        /// </summary>
        /// <param name="agencySearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns>Tuple<List<AgencyDTO>,int></returns>
        Tuple<List<AgencyDTO>, int> GetAgencyList(AgencySearchDTO agencySearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);

        /// <summary>
        /// GetAgencyCount.
        /// </summary>
        /// <returns></returns>
        int GetAgencyCount();

        /// <summary>
        /// GetAgencyDetails.
        /// </summary>
        /// <param name="agencyIndex">agencyIndex.</param>
        /// <returns>GetAgencyDetailsDTO.</returns>
        AgencyDataDTO GetAgencyDetails(Guid agencyIndex);

        /// <summary>
        /// GetAllAgencyLookup.
        /// </summary>
        /// <returns></returns>
        List<AgencyLookupDTO> GetAllAgencyLookup();

        /// <summary>
        /// ValidateAgencyName.
        /// </summary>
        /// <param name="agencyDetailsDTO">agencyDetailsDTO.</param>
        /// <returns>bool.</returns>
        Task<bool> ValidateAgencyName(AgencyDetailsDTO agencyDetailsDTO);

        /// <summary>
        /// ValidateAgencyAbbrev.
        /// </summary>
        /// <param name="agencyDetailsDTO">agencyDetailsDTO.</param>
        /// <returns>bool.</returns>
        Task<bool> ValidateAgencyAbbrev(AgencyDetailsDTO agencyDetailsDTO);

        Task<AgencyDTO> GetAgencyDetailsByAbbrev(string abbrev);

        Task<AgencyDTO> GetAgencyDetailsById(long agencyId);

        /// <summary>
        /// GetAgencyLookupWithID.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>AgencyLookupDTO List.</returns>
        List<AgencyLookupDTO> GetAgencyLookupWithID(long agencyID);

        /// <summary>
        /// GetAllAgencyForSharing.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>AgencyLookupDTO List.</returns>
        List<AgencyLookupDTO> GetAllAgencyForSharing();

        /// <summary>
        /// GetAgencyListForOrgAdmin.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        List<AgencyDTO> GetAgencyListForOrgAdmin(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetAgencyListForOrgAdminCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        int GetAgencyListForOrgAdminCount(long agencyID);
    }
}
