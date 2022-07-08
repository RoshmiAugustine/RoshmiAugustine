// -----------------------------------------------------------------------
// <copyright file="NotifyRiskRule.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class NotifyRiskRule : BaseEntity
    {
        public int NotifyRiskRuleID { get; set; }
        public int QuestionnaireItemID { get; set; }
        public int NotifyThresholdMinimumListOrder { get; set; }
        public int NotificationLevelID { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public NotificationLevel NotificationLevel { get; set; }
        public QuestionnaireItem QuestionnaireItem { get; set; }
        public User UpdateUser { get; set; }
    }
}
