using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface ICollaborationLevelRepository
    {
        /// <summary>
        /// GetCollaborationLevelList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>List of CollaborationLevelDTO</returns>
        List<CollaborationLevelDTO> GetCollaborationLevelList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetCollaborationLevelCount.
        /// </summary>
        /// <returns>int.</returns>
        int GetCollaborationLevelCount(long agencyID);

        /// <summary>
        /// AddCollaborationLevel.
        /// </summary>
        /// <param name="CollaborationLevelDTO">CollaborationLevelDTO.</param>
        /// <returns>CollaborationLevelDTO.</returns>
        CollaborationLevelDTO AddCollaborationLevel(CollaborationLevelDTO CollaborationLevelDTO);

        /// <summary>
        /// UpdateCollaborationLevel.
        /// </summary>
        /// <param name="CollaborationLevelDTO">CollaborationLevelDTO.</param>
        /// <returns>CollaborationLevelDTO.</returns>
        CollaborationLevelDTO UpdateCollaborationLevel(CollaborationLevelDTO CollaborationLevelDTO);

        /// <summary>.
        /// GetCollaborationLevel
        /// </summary>
        /// <param name="collaborationLevelID">collaborationLevelID.</param>
        /// <returns>Task of CollaborationLevelDTO.</returns>
        Task<CollaborationLevelDTO> GetCollaborationLevel(Int64 collaborationLevelID);
    }
}
