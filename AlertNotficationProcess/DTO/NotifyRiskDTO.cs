// -----------------------------------------------------------------------
// <copyright file="NotificationTypeDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace AlertNotficationProcess.DTO
{
    public class NotifyRiskDTO
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
    }
}
