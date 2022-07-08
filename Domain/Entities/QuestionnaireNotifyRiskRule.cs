// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRule.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireNotifyRiskRule : BaseEntity
    {
        public int QuestionnaireNotifyRiskRuleID { get; set; }
        public string Name { get; set; }
        public int QuestionnaireNotifyRiskScheduleID { get; set; }
        public int NotificationLevelID { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? ClonedQuestionnaireNotifyRiskRuleID { get; set; }


        public NotificationLevel NotificationLevel { get; set; }
        public QuestionnaireNotifyRiskSchedule QuestionnaireNotifyRiskSchedule { get; set; }
        public User UpdateUser { get; set; }
    }
}
