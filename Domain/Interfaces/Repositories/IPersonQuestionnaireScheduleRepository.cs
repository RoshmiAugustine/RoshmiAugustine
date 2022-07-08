// -----------------------------------------------------------------------
// <copyright file="IPersonQuestionnaireRepository.cs" company="Naicoits">
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
    /// IAgencyRepository.
    /// </summary>
    public interface IPersonQuestionnaireScheduleRepository : IAsyncRepository<PersonQuestionnaireSchedule>
    {
        /// <summary>
        /// To add personQuestionnaireSchedule details.
        /// </summary>
        /// <param name="personQuestionnaireDTO"></param>
        /// <returns>Guid.</returns>
        long AddPersonQuestionnaireSchedule(PersonQuestionnaireSchedule personQuestionnaireSchedule);
        List<PersonQuestionnaireSchedule> UpdateBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireSchedule> personQuestionnaireSchedule);
        Task<List<PersonQuestionnaireSchedule>> GetPersonQuestionnaireSchedule(long personQuestionnaireID, int questionnaireWindowID);
        PersonQuestionnaireSchedule UpdatePersonQuestionnaireSchedule(PersonQuestionnaireSchedule personQuestionnaireSchedule);
        List<ReminderNotificationsListDTO> GetNotifyReminderScheduledForToday(DateTime? lastRunTime = null, DateTime? currentRunTime = null);
        int GetNotifyReminderScheduledCountForToday(DateTime lastRunTime, DateTime currentRunTime);
        Task<List<PersonQuestionnaireSchedule>> GetPersonQuestionnaireScheduleByWindowList(long personQuestionnaireID, List<int> questionnaireWindowIDList);
        Task<List<PersonQuestionnaireSchedule>> GetPersonQuestionnaireScheduleList(DateTime dateTaken, List<int> questionnaireWindowIDs, long personQuestionnaireID);
        List<PersonQuestionnaireRegularScheduleDetailsDTO> GetAllPersonQuestionnairesRegularSchedule(string value);
        /// <summary>
        /// AddBulkPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="personQuestionnaireSchedule"></param>
        /// <returns></returns>
        List<PersonQuestionnaireSchedule> AddBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireSchedule> personQuestionnaireSchedule);
        /// <summary>
        /// GetAllPersonQuestionnaireScheduleID From Index
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        List<PersonQuestionnaireSchedule> GetAllPersonQuestionnaireScheduleID(List<Guid?> lists);
        /// <summary>
        /// GetAllRegularSchedulesWithWindowOpenToday.
        /// Retrieve all last occurence reminders for scheduling the next occurence reminders by FutureReminderNotificationProcess.PCIS-3225
        /// </summary>
        /// <returns></returns>
        List<PersonQuestionnaireRegularScheduleDetailsDTO> GetAllRegularSchedulesWithWindowOpenToday();
        List<PersonQuestionnaireRegularScheduleDetailsDTO> GetAllLatestSchedulesWithMaXDueDate(List<long> lst_PersonQuestionnaireIds);
        List<PersonQuestionnaireDTO> GetAllPersonQuestionnairesToBeScheduled();
        List<PersonQuestionnaireSchedule> GetPersonQuestionnaireSchedule(List<long> personQuestionnaireIDs);
    }
}
