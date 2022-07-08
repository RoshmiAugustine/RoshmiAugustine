// -----------------------------------------------------------------------
// <copyright file="RiskNotificationsListDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace AlertAutoResolveProcess.DTO
{
    public class RiskNotificationsListDTO
    {
        public int NotificationLogID { get; set; }
        public string NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public long PersonID { get; set; }
        public bool IsRemoved { get; set; }
        public int NotificationTypeID { get; set; }
        public int NotifyRiskID { get; set; }
        public int QuestionnaireNotifyRiskRuleID { get; set; }
        public int AssessmentID { get; set; }
        public DateTime? NotifyDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool RiskIsRemoved { get; set; }
        public string Details { get; set; }
        public int NotificationResolutionStatusID { get; set; }
        public int? FKeyValue { get; set; }
        public string NotificationData { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime? StatusDate { get; set; }
        public string? HelperName { get; set; }
    }
}
