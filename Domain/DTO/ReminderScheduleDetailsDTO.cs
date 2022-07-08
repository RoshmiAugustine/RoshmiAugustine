// -----------------------------------------------------------------------
// <copyright file="AddressDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class ReminderScheduleDetailsDTO
    {
        public string ReminderScheduleName { get; set; }
        public bool IsEmailRemindersHelpers { get; set; }
        public bool IsAlertsHelpersManagers { get; set; }
        public List<QuestionnaireWindowDTO> QuestionnaireWindow { get; set; }

        public List<QuestionnaireReminderRuleDTO> QuestionnaireReminderRule { get; set; }

        public bool IsEmailInviteToCompleteReminders { get; set; }
        public string InviteToCompleteReceiverIds { get; set; }

        public List<QuestionnaireRegularReminderTimeRuleDTO> questionnaireRegularReminderTime { get; set; }
    }
}
