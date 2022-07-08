// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRulesDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace AlertNotficationProcess.DTO
{
    public class QuestionnaireNotifyRiskRulesDTO
    {
        public int QuestionnaireNotifyRiskRuleID { get; set; }

        public string Name { get; set; }

        public int QuestionnaireNotifyRiskScheduleID { get; set; }

        public int NotificationLevelID { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserID { get; set; }

        public int? ClonedQuestionnaireNotifyRiskRuleID { get; set; }

    }
}
