// -----------------------------------------------------------------------
// <copyright file="INotifiationResolutionStatusRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// INotifiationResolutionStatusRepository.
    /// </summary>
    public interface INotifiationResolutionStatusRepository : IAsyncRepository<NotificationResolutionStatus>
    {
        /// <summary>
        /// GetNotificationStatus
        /// </summary>
        /// <returns>NotificationResolutionStatusDTO</returns>
        NotificationResolutionStatus GetNotificationStatus(string status);

        /// <summary>
        /// GetNotificationStatusById
        /// </summary>
        /// <returns>NotificationResolutionStatusDTO</returns>
        NotificationResolutionStatus GetNotificationStatusById(int statusId);

        /// <summary>
        /// Get Notification Status For Unresolved
        /// </summary>
        /// <returns></returns>
        NotificationResolutionStatusDTO GetNotificationStatusForUnResolved();

        /// <summary>
        /// Get Notification Status For Resolved
        /// </summary>
        /// <returns></returns>
        NotificationResolutionStatusDTO GetNotificationStatusForResolved();
    }
}
