using System.Collections.Generic;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface INotificationLevelRepository
    {
        /// <summary>
        /// GetNotificationLevelList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>List of NotificationLevelDTO</returns>
        List<NotificationLevelDTO> GetNotificationLevelList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetNotificationLevelCount.
        /// </summary>
        /// <returns>int.</returns>
        int GetNotificationLevelCount(long agencyID);

        /// <summary>
        /// AddNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelDTO">NotificationLevelDTO.</param>
        /// <returns>NotificationLevelDTO.</returns>
        NotificationLevelDTO AddNotificationLevel(NotificationLevelDTO notificationLevelDTO);

        /// <summary>
        /// UpdateNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelDTO">NotificationLevelDTO.</param>
        /// <returns>NotificationLevelDTO.</returns>
        NotificationLevelDTO UpdateNotificationLevel(NotificationLevelDTO notificationLevelDTO);

        /// <summary>.
        /// GetNotificationLevel
        /// </summary>
        /// <param name="notificationLevelID">collaborationLevelID.</param>
        /// <returns>Task of NotificationLevelDTO.</returns>
        Task<NotificationLevelDTO> GetNotificationLevel(int notificationLevelID);

        /// <summary>
        /// GetNotificationLevelCountByNotificationLevelID
        /// </summary>
        /// <param name="notificationLevelID">notificationLevelID</param>
        /// <returns>int</returns>
        int GetNotificationLevelCountByNotificationLevelID(int notificationLevelID);
    }
}
