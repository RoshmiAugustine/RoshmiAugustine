// -----------------------------------------------------------------------
// <copyright file="IAgencySharingRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAgencySharingRepository
    {
        /// <summary>
        /// AddAgencySharing
        /// </summary>
        /// <param name="partnerAgencyDTO"></param>
        /// <returns>AgencySharingDTO</returns>
        AgencySharingDTO AddAgencySharing(AgencySharingDTO partnerAgencyDTO);
        /// <summary>
        /// To update AgencySharing details.
        /// </summary>
        /// <param name="agencySharingDTO">id.</param>
        /// <returns>List of summaries.</returns>
        AgencySharing UpdateAgencySharing(AgencySharing agencySharingDTO);
        /// <summary>
        /// To get details AgencySharing.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        Task<AgencySharing> GetAgencySharing(Guid id);
    }
}
