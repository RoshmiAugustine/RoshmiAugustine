// -----------------------------------------------------------------------
// <copyright file="ICollaborationSharingPolicyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface ICollaborationSharingPolicyRepository
    {
        /// <summary>
        /// AddCollaborationSharingPolicy
        /// </summary>
        /// <param name="collaborationSharingPolicyDTO"></param>
        /// <returns>CollaborationSharingPolicyDTO</returns>
        CollaborationSharingPolicyDTO AddCollaborationSharingPolicy(CollaborationSharingPolicyDTO collaborationSharingPolicyDTO);

        /// <summary>
        /// To get details CollaborationSharingPolicy.
        /// </summary>
        /// <param "Guid>id.</param>
        /// <returns>.AgencyPersonCollaborationDTO</returns>
        Task<CollaborationSharingPolicyDTO> GetCollaborationSharingPolicy(int id);

        /// <summary>
        /// To update CollaborationSharingPolicy details.
        /// </summary>
        /// <param name="collaborationSharingPolicyDTO">id.</param>
        /// <returns>List of summaries.</returns>
        CollaborationSharingPolicyDTO UpdateCollaborationSharingPolicy(CollaborationSharingPolicyDTO collaborationSharingPolicyDTO);

        /// <summary>
        /// GetAgencySharingPolicy.
        /// </summary>
        /// <returns>CollaborationSharingPolicyDTO.</returns>
        Task<List<CollaborationSharingPolicyDTO>> GetAllCollaborationSharingPolicy();

    }
}
