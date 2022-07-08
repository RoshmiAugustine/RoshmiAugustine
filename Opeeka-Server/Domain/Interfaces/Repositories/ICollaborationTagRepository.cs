// -----------------------------------------------------------------------
// <copyright file="ICollaborationTag.cs" company="Naicoits">
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
    /// ICollaborationTag.
    /// </summary>
    public interface ICollaborationTagRepository : IAsyncRepository<CollaborationTag>
    {
        /// <summary>
        /// To add CollaborationTag.
        /// </summary>
        /// <param name="collaborationTagDTO">collaborationTagDTO.</param>
        /// <returns>id.</returns>
        Int64 AddCollaborationTag(CollaborationTagDTO collaborationTagDTO);

        /// <summary>
        /// To update collaboration tag details.
        /// </summary>
        /// <param name="CollaborationTagDTO">CollaborationTagDTO.</param>
        /// <returns>CollaborationTagDTO.</returns>
        CollaborationTagDTO UpdateCollaborationTag(CollaborationTagDTO collaborationTagDTO);

        /// <summary>
        /// GetCollaborationCategories.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>CollaborationTagDTO List.</returns>
        List<CollaborationTagDTO> GetCollaborationCategories(int collaborationID);

        /// <summary>
        /// Get the count of Collaboration tags having a specific tag type
        /// </summary>
        /// <param name="collaborationTagTypeID"></param>
        /// <returns>int</returns>
        int GetCollaborationTagTypeCountByCollaborationTag(int collaborationTagTypeID);
    }
}
