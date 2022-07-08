// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireScheduleService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Infrastructure.Enums;
using System.Linq;
using AutoMapper;
using Opeeka.PICS.Domain.DTO.Input;

namespace Opeeka.PICS.Domain.Services
{
    public class PersonQuestionnaireScheduleService : BaseService, IPersonQuestionnaireScheduleService
    {
        private IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository;
        private readonly INotifyReminderRepository notifyReminderRepository;
        private readonly INotificationTypeRepository notificationTypeRepository;

        private readonly IMapper mapper;
        /// <summary>
        /// Defines the NotificationLogRepository.
        /// </summary>
        private readonly INotificationLogRepository notificationLogRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageService"/> class.
        /// </summary>
        /// <param name="personQuestionnaireMetricsRepository"></param>
        /// <param name="localizeService"></param>
        /// <param name="configRepo"></param>
        /// <param name="httpContext"></param>
        /// <param name="querybuild"></param>
        public PersonQuestionnaireScheduleService(IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, INotifyReminderRepository notifyReminderRepository, INotificationLogRepository notificationLogRepository, INotificationTypeRepository notificationTypeRepository, IMapper mapper)
            : base(configRepo, httpContext)
        {
            this.personQuestionnaireScheduleRepository = personQuestionnaireScheduleRepository;
            this.notifyReminderRepository = notifyReminderRepository;
            this.notificationLogRepository = notificationLogRepository;
            this.notificationTypeRepository = notificationTypeRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get Dashboard Strength Metrics
        /// </summary>
        /// <param name="strengthMetricsSearchDTO"></param>
        /// <returns>DashboardStrengthMetricsListResponseDTO</returns>
        public bool UpdatePersonQuestionnaireSchedule(PersonQuestionnaireSchedule personQuestionnaireSchedule)
        {
            try
            {
                var result = personQuestionnaireScheduleRepository.UpdatePersonQuestionnaireSchedule(personQuestionnaireSchedule);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Dashboard Strength Metrics
        /// </summary>
        /// <param name="strengthMetricsSearchDTO"></param>
        /// <returns>DashboardStrengthMetricsListResponseDTO</returns>
        public bool UpdateBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireSchedule> personQuestionnaireScheduleList)
        {
            try
            {
                RemoveReminderSchedules(personQuestionnaireScheduleList);
                var result = personQuestionnaireScheduleRepository.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleList);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long AddPersonQuestionnaireSchedule(PersonQuestionnaireSchedule personQuestionnaireSchedule)
        {
            try
            {
                var result = personQuestionnaireScheduleRepository.AddPersonQuestionnaireSchedule(personQuestionnaireSchedule);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To delete reminder schedules
        /// </summary>
        /// <param name="personQuestionnaireScheduleList"></param>
        public void RemoveReminderSchedules(List<PersonQuestionnaireSchedule> personQuestionnaireScheduleList)
        {
            try
            {
                if (personQuestionnaireScheduleList?.Count > 0)
                {
                    var personQuestionnaireScheduleIsRemoved = personQuestionnaireScheduleList.Where(x => x.IsRemoved == true);
                    var notifyReminderIdToBeRemoved = new GetNotifyReminderInputDTO() { personQuestionnaireScheduleIDList = personQuestionnaireScheduleIsRemoved.Select(x => x.PersonQuestionnaireScheduleID).ToList() };
                    var removedNotifyRemindersDTO = this.notifyReminderRepository.GetNotifyReminders(notifyReminderIdToBeRemoved,false);
                    //Update All NotifyReminders to be removed.
                    List<NotifyReminder> notifyReminders = new List<NotifyReminder>();
                    this.mapper.Map<List<NotifyReminderDTO>, List<NotifyReminder>>(removedNotifyRemindersDTO, notifyReminders);

                    if (notifyReminders.Count > 0)
                    {
                        notifyReminders?.ForEach(x => x.IsRemoved = true);
                        this.notifyReminderRepository.UpdateBulkNotifyReminder(notifyReminders);
                        var notificationType = this.notificationTypeRepository.GetNotificationType(PCISEnum.NotificationType.Reminder).Result;
                        //get All NotificationLogs for reminders associated with removed NotifyReminderIDs.
                        var notificationLog = this.notificationLogRepository.GetNotifcationLogForReminder(notifyReminders?.Select(x => x.NotifyReminderID).ToList(), notificationType.NotificationTypeID);
                        //Update all NotificationLogs to be removed
                        notificationLog?.ForEach(x => x.IsRemoved = true);
                        this.notificationLogRepository.UpdateBulkNotificationLog(notificationLog);
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
