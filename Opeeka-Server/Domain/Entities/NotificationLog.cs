// -----------------------------------------------------------------------
// <copyright file="NotificationLog.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class NotificationLog : BaseEntity
    {
        public int NotificationLogID { get; set; }
        public DateTime NotificationDate { get; set; }
        public long PersonID { get; set; }
        public int NotificationTypeID { get; set; }
        public int? FKeyValue { get; set; }
        public string? NotificationData { get; set; }
        public int NotificationResolutionStatusID { get; set; }
        public DateTime? StatusDate { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? QuestionnaireID { get; set; }
        public int? AssessmentID { get; set; }
        public string? Details { get; set; }
        public string? HelperName { get; set; }

        public Person Person { get; set; }
        public User UpdateUser { get; set; }
        public NotificationResolutionStatus NotificationResolutionStatus { get; set; }
        public NotificationType NotificationType { get; set; }
        public int? AssessmentNoteID { get; set; }
    }
}
