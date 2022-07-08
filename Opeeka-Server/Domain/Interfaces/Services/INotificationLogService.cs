// -----------------------------------------------------------------------
// <copyright file="INotificationLogService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface INotificationLogService
    {
        /// <summary>
        /// Get Notification Log List
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <returns></returns>
        NotificationLogResponseDTO GetNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO);

        /// <summary>
        /// Get Past Notification Log List
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <returns>NotificationLogResponseDTO</returns>
        NotificationLogResponseDTO GetPastNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO);

        /// <summary>
        /// AddBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO AddBulkNotificationLog(List<NotificationLogDTO> notificationLog);

        /// <summary>
        /// GetNotificationLogForNotificationResolveAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        NotificationLogResponseDTO GetNotificationLogForNotificationResolveAlert(long personId);

        /// <summary>
        /// UpdateBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateBulkNotificationLog(List<NotificationLogDTO> notificationLog);

        /// <summary>
        /// GetUnResolvedNotificationLogForNotificationResolveAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>RiskNotificationResponseDTO.</returns>
        RiskNotificationResponseDTO GetUnResolvedNotificationLogForNotificationResolveAlert(long personId);

        /// <summary>
        /// NotificationCountIndication
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="lastLoginTime"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        NotificationLogResponseDTO NotificationCountIndication(NotificationLogSearchDTO notificationLogSearchDTO, int userId);
    }
}