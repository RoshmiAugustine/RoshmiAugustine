// -----------------------------------------------------------------------
// <copyright file="ICollaborationTagTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// ICollaborationTagTypeRepository.
    /// </summary>
    public interface ICollaborationTagTypeRepository
    {
        /// <summary>
        /// Get all Collaboration Tag Types
        /// </summary>
        /// <returns>CollaborationTagTypeDTO</returns>
        Task<List<CollaborationTagTypeDTO>> GetAllCollaborationTagType();

        /// <summary>
        /// Get Collaboration Tag Type list paginated
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>CollaborationTagTypeDTO</returns>
        List<CollaborationTagTypeDTO> GetCollaborationTagTypeList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// Get the total count of Collaboration Tag Types
        /// </summary>
        /// <returns>int</returns>
        int GetCollaborationTagTypeCount(long agencyID);

        /// <summary>
        /// Add a new Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeDetailsDTO"></param>
        /// <returns>CollaborationTagTypeDTO</returns>
        CollaborationTagTypeDTO AddCollaborationTagType(CollaborationTagTypeDTO collaborationTagTypeDetailsDTO);

        /// <summary>
        /// Update an existing Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeDetailsDTO"></param>
        /// <returns>collaborationTagTypeDTO</returns>
        CollaborationTagTypeDTO UpdateCollaborationTagType(CollaborationTagTypeDTO collaborationTagTypeDetailsDTO);

        /// <summary>
        /// Get one Collaboration Tag Type by Id
        /// </summary>
        /// <param name="collaborationTagTypeID"></param>
        /// <returns>Task<CollaborationTagTypeDTO></returns>
        Task<CollaborationTagTypeDTO> GetCollaborationTagType(int collaborationTagTypeID);
    }
}
