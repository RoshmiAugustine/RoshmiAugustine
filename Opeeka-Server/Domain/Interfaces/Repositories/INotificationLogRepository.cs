// -----------------------------------------------------------------------
// <copyright file="INotificationLogRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// INotificationLogRepository.
    /// </summary>
    public interface INotificationLogRepository : IAsyncRepository<NotificationLog>
    {
        /// <summary>
        /// UpdateNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>NotificationLog</returns>
        NotificationLog UpdateNotificationLog(NotificationLog notificationLog);

        /// <summary>
        /// GetNotificationLog.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <returns>NotificationLogDTO.</returns>
        Task<NotificationLogDTO> GetNotificationLog(int notificationLogID);

        /// <summary>
        /// GetNotificationLogByID.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <returns>NotificationLogDTO.</returns>
        public NotificationLogDTO GetNotificationLogByID(int notificationLogID);

        /// <summary>
        /// GetReminderNotifications.
        /// </summary>
        /// <param name="notifyReminderId">notifyReminderId.</param>
        /// <returns>NotificationLogDTO.</returns>
        public List<NotificationLogDTO> GetReminderNotifications(int notifyReminderId);

        /// <summary>
        /// Get Notification Log List
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns>Tuple<List<NotificationLogDTO>, int></returns>
        Tuple<List<NotificationLogDTO>, int> GetNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);

        /// <summary>
        /// AddNotificationLog
        /// </summary>
        /// <param name="notificationLog"></param>
        /// <returns>NotificationLog</returns>
        NotificationLog AddNotificationLog(NotificationLog notificationLog);

        IReadOnlyList<NotificationLog> GetAssessmentNotificationLog(List<int> notificationTypeIDs, int assessmentId);

        void AddBulkNotificationLog(List<NotificationLog> notificationLog);

        /// <summary>
        /// Get Past Notification Log List
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns>Tuple<List<NotificationLogDTO>, int></returns>
        Tuple<List<NotificationLogDTO>, int> GetPastNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);

        /// <summary>
        /// Get NotificationLog For Notification ResolveAlert
        /// </summary>
        /// <param name="PersonID"></param>
        /// <returns></returns>
        List<NotificationLog> GetNotificationLogForNotificationResolveAlert(long PersonID);

        /// <summary>
        /// Get UnResolved NotificationLog For Notification ResolveAlert
        /// </summary>
        /// <param name="PersonID"></param>
        /// <returns></returns>
        List<RiskNotificationsListDTO> GetUnResolvedNotificationLogForNotificationResolveAlert(long PersonID);

        /// <summary>
        /// Update Notification LogById
        /// </summary>
        /// <param name="NotificationLog"></param>
        /// <returns></returns>
        public List<NotificationLog> UpdateNotificationLog(List<NotificationLog> NotificationLog);

        /// <summary>
        /// Get Past Notifications
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="personID"></param>
        /// <returns>Tuple<List<NotificationLogDTO>, int></returns>
        Tuple<List<NotificationLogDTO>, int> GetPastNotifications(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO, long personID, SharedDetailsDTO sharedIDs, long personAgencyID, SharedDetailsDTO helperColbIDs);

        /// <summary>
        /// Get Present Notifications
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="personID"></param>
        /// <returns>Tuple<List<NotificationLogDTO>, int></returns>
        Tuple<List<NotificationLogDTO>, int> GetPresentNotifications(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO, long personID, SharedDetailsDTO sharedIDs, long personAgencyID, SharedDetailsDTO helperColbIDs);
        List<NotificationLog> UpdateBulkNotificationLog(List<NotificationLog> notificationLogList);
        Tuple<List<NotificationLogDTO>, int> GetDashboardNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);
        /// <summary>
        /// UpdateBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>NotificationLog</returns>
        List<NotificationLog> GetNotifcationLogForReminder(List<int> notifyReminderIDList, int notificationTypeID);

        /// <summary>
        /// GetNotificationCount
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="lastLogin"></param>
        /// <returns></returns>
        int GetNotificationCount(NotificationLogSearchDTO notificationLogSearchDTO, DateTime lastLogin);
        /// <summary>
        /// GetAssessmentAlertNotificationLog
        /// </summary>
        /// <param name="notificationTypeID"></param>
        /// <param name="assessmentId"></param>
        /// <returns></returns>
        IReadOnlyList<NotificationLog> GetAssessmentAlertNotificationLog(int notificationTypeID, int assessmentId);
    }
}
