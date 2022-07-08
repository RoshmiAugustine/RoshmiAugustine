// -----------------------------------------------------------------------
// <copyright file="IConsumerService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// IAgencyService.
    /// </summary>
    public interface IAgencyService
    {
        /// <summary>
        /// Add Agency Details.
        /// </summary>
        /// <param name="agencyDetailsDTO">id.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddAgencyDetails(AgencyDetailsDTO agencyDetailsDTO);

        /// <summary>
        /// Update Agency Details.
        /// </summary>
        /// <param name="agencyDetailsDTO">id.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateAgencyDetails(AgencyDetailsDTO agencyDetailsDTO);

        /// <summary>
        /// GetAgencyList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="roles">roles.</param>
        /// <returns></returns>
        GetAgencyListDTO GetAgencyList(AgencySearchDTO personSearchDTO, long agencyID, List<string> roles);

        /// <summary>
        /// GetAgencyDetails
        /// </summary>
        /// <param name="agencyIndex"></param>
        /// <returns></returns>
        GetAgencyDetailsDTO GetAgencyDetails(Guid agencyIndex);

        /// <summary>
        /// remove Agency Details.
        /// </summary>
        /// <param name="AgencyIndexId">id.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO RemoveAgencyDetails(Guid AgencyIndexId);

        /// <summary>
        /// Get All Agency Lookup
        /// </summary>
        /// <returns></returns>
        AgencyLookupResponseDTO GetAllAgencyLookup();
    }
}
