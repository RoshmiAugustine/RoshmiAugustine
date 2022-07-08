// -----------------------------------------------------------------------
// <copyright file="IAgencySharingPolicyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAgencySharingPolicyRepository
    {
        /// <summary>
        /// AddAgencySharingPolicy
        /// </summary>
        /// <param name="agencySharingDTO"></param>
        /// <returns>AgencySharingPolicyDTO</returns>
        AgencySharingPolicyDTO AddAgencySharingPolicy(AgencySharingPolicyDTO agencySharingDTO);

        /// <summary>
        /// To get details AgencySharingPolicy.
        /// </summary>
        /// <param AgencySharingPolicyDTO="AgencySharingPolicyDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        Task<AgencySharingPolicyDTO> GetAgencySharingPolicy(long id);

        /// <summary>
        /// To update AgencySharing details.
        /// </summary>
        /// <param name="agencySharingPolicyDTO">id.</param>
        /// <returns>List of summaries.</returns>
        AgencySharingPolicyDTO UpdateAgencySharingPolicy(AgencySharingPolicyDTO agencySharingPolicyDTO);

        /// <summary>
        /// GetAgencySharingPolicy
        /// </summary>
        /// <returns></returns>
        Task<List<AgencySharingPolicyDTO>> GetAgencySharingPolicy();
    }
}
