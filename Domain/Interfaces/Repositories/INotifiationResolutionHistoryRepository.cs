// -----------------------------------------------------------------------
// <copyright file="INotifiationResolutionHistoryRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// INotifiationResolutionHistoryRepository.
    /// </summary>
    public interface INotifiationResolutionHistoryRepository : IAsyncRepository<NotificationResolutionHistory>
    {
        /// <summary>
        /// AddNotificationResolutionHistory.
        /// </summary>
        /// <param name="notificationResolutionHistory">notificationResolutionHistory.</param>
        /// <returns>NotificationResolutionHistory.</returns>
        NotificationResolutionHistory AddNotificationResolutionHistory(NotificationResolutionHistory notificationResolutionHistory);

        /// <summary>
        /// UpdateBulkNotificationResolutionHistory.
        /// </summary>
        /// <param name="NotificationResolutionHistory">NotificationResolutionHistory.</param>
        /// <returns>NotificationLog</returns>
        void AddBulkNotificationResolutionHistory(List<NotificationResolutionHistory> notificationResolutionHistoryList);
    }
}
