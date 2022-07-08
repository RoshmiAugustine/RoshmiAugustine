// -----------------------------------------------------------------------
// <copyright file="ScheduleParameterDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class ScheduleParameterDTO
    {
        public int QuestionnaireID { get; set; }

        public string ReminderScheduleName { get; set; }
        public bool IsEmailRemindersHelpers { get; set; }
        public bool IsAlertsHelpersManagers { get; set; }
        public bool HasSkipLogic { get; set; }
        public bool HasDefaultResponseRule { get; set; }
        public int UpdatedUserID { get; set; }        
        public List<QuestionnaireWindowDTO> QuestionnaireWindow { get; set; }

        public List<QuestionnaireReminderRuleDTO> QuestionnaireReminderRule { get; set; }

        public QuestionnaireNotifyRiskScheduleDTO QuestionnaireNotifyRiskSchedule { get; set; }

        public List<QuestionnaireSkipLogicRuleDTO> QuestionnaireSkipLogicRules { get; set; }
        public List<QuestionnaireDefaultResponseRuleDTO> QuestionnaireDefaultResponseRules { get; set; }

        public List<QuestionnaireRegularReminderTimeRuleDTO> questionnaireRegularReminderTime { get; set; }
        public bool IsEmailInviteToCompleteReminders { get; set; }
        public string InviteToCompleteReceiverIds { get; set; }

    }
}
