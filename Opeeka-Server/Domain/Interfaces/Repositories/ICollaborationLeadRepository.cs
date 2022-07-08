// -----------------------------------------------------------------------
// <copyright file="ICollaborationLeadRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// ICollaborationLeadRepository.
    /// </summary>
    public interface ICollaborationLeadRepository : IAsyncRepository<CollaborationLeadHistory>
    {
        /// <summary>
        /// To add CollaborationLead.
        /// </summary>
        /// <param name="collaborationTagDTO">collaborationTagDTO.</param>
        /// <returns>id.</returns>
        Int64 AddCollaborationLead(CollaborationLeadHistoryDTO collaborationTagDTO);

        /// <summary>
        /// To update collaboration lead history details.
        /// </summary>
        /// <param name="CollaborationLeadHistoryDTO">CollaborationLeadHistoryDTO.</param>
        /// <returns>CollaborationLeadHistoryDTO.</returns>
        CollaborationLeadHistoryDTO UpdateCollaborationLeadHistory(CollaborationLeadHistoryDTO collaborationLeadHistoryDTO);

        /// <summary>
        /// GetCollaborationLeads.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>CollaborationLeadHistoryDTO List.</returns>
        List<CollaborationLeadHistoryDTO> GetCollaborationLeads(int collaborationID);
    }
}
