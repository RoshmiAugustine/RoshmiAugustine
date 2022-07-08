// -----------------------------------------------------------------------
// <copyright file="ReminderScheduleResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace ReminderTriggerProcess.DTO
{
    public class ReminderNotificationScheduleResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public int Count { get; set; }
        public List<PersonQuestionnaireScheduleEmailDTO> personQuestionnaireScheduleEmails { get; set; }
        public List<ReminderNotificationsListDTO> ReminderNotification { get; set; }
    }
    public class ReminderNotificationScheduleResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public ReminderNotificationScheduleResponse result { get; set; }
    }
}
