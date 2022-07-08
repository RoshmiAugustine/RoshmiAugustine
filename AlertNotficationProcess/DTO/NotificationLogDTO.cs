// -----------------------------------------------------------------------
// <copyright file="NotificationLogDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace AlertNotficationProcess.DTO
{
    public class NotificationLogDTO
    {
        public int NotificationLogID { get; set; }
        public DateTime NotificationDate { get; set; }
        public long PersonID { get; set; }
        public int NotificationTypeID { get; set; }
        public int? FKeyValue { get; set; }
        public string NotificationData { get; set; }
        public int NotificationResolutionStatusID { get; set; }
        public DateTime? StatusDate { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public string NotificationType { get; set; }
        public string PersonName { get; set; }
        public Guid PersonIndex { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public int QuestionnaireID { get; set; }
        public int AssessmentID { get; set; }
        public int? AssessmentNoteID { get; set; }
        public string HelperName { get; set; }
    }
}
