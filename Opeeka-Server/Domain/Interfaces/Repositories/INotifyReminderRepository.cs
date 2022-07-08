// -----------------------------------------------------------------------
// <copyright file="INotificationLogRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// INotificationLogRepository.
    /// </summary>
    public interface INotifyReminderRepository : IAsyncRepository<NotifyReminder>
    {
        List<NotifyReminder> AddBulkNotifyReminder(List<NotifyReminderDTO> notifyReminderList);
        void UpdateBulkNotifyReminder(List<NotifyReminder> notifyReminderList);
        /// <summary>
        /// GetNotifyReminders.
        /// </summary>
        /// <param name="GetNotifyReminderInput">GetNotifyReminderInput.</param>
        /// <returns>NotifyReminderDetailsDTO.</returns>
        public List<NotifyReminderDTO> GetNotifyReminders(GetNotifyReminderInputDTO GetNotifyReminderInput, bool fetchIsLogAdded = true);
        List<RemindersToTriggerInviteToCompleteDTO> GetReminderDetailsForInviteToCompleteTrigger();
        List<NotifyReminder> GetNotifyRemindersByIds(List<int> reminderIds);
    }
}
