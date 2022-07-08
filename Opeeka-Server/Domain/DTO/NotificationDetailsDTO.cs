// -----------------------------------------------------------------------
// <copyright file="NotificationDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class NotificationDetailsDTO
    {
        public int QuestionnaireNotifyRiskScheduleID { get; set; }
        public string NotificationName { get; set; }
        public int QuestionnaireID { get; set; }
        public int QuestionnaireNotifyRiskRuleID { get; set; }
        public string RuleName { get; set; }
        public int QuestionnaireNotifyRiskRuleConditionID { get; set; }
        public int QuestionnaireItemID { get; set; }
        public int NotificationLevelID { get; set; }
        public decimal ComparisonValue { get; set; }
        public int ListOrder { get; set; }
        public string ComparisonOperator { get; set; }
        public string JoiningOperator { get; set; }
        public bool IsAlertsHelpersManagers { get; set; }

    }
}
