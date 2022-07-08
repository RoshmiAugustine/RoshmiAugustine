// -----------------------------------------------------------------------
// <copyright file="ReminderScheduleResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class ReminderNotificationScheduleResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public int Count { get; set; }
        public List<PersonQuestionnaireScheduleEmailDTO> PersonQuestionnaireScheduleEmails { get; set; }

        public List<ReminderNotificationsListDTO> ReminderNotification { get; set; }
    }
}
