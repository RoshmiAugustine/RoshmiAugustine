// -----------------------------------------------------------------------
// <copyright file="NotifyRisk.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class NotifyRisk : BaseEntity
    {
        public int NotifyRiskID { get; set; }

        public int QuestionnaireNotifyRiskRuleID { get; set; }

        public long PersonID { get; set; }
        public int AssessmentID { get; set; }

        public DateTime? NotifyDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserID { get; set; }

        public Person Person { get; set; }
        public Assessment Assessment { get; set; }
        public QuestionnaireNotifyRiskRule QuestionnaireNotifyRiskRule { get; set; }
        public User UpdateUser { get; set; }

    }
}
