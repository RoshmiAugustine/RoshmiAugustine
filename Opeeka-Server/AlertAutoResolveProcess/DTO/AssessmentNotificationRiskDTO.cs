// -----------------------------------------------------------------------
// <copyright file="AssessmentResponsesDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AlertAutoResolveProcess.DTO;
using System.Collections.Generic;

namespace AlertAutoResolveProcess.DTO
{
    public class AssessmentNotificationRiskDTO
    {
        public int AssessmentID { get; set; }
        public int ResponseID { get; set; }
        public int QuestionnaireItemID { get; set; }
        public long PersonID { get; set; }
        public int UpdateUserID { get; set; }
        public int NotificationResolutionStatusID { get; set; }
        public int QuestionnaireNotifyRiskRuleID { get; set; }
        public int NotificationTypeID { get; set; }
        public List<NotifyRiskValueDTO> NotifyRiskValueList { get; set; }
        public string EmailAttribute { get; set; }
        public int QuestionnaireID { get; set; }
        public string Detail { get; set; }
    }
}
