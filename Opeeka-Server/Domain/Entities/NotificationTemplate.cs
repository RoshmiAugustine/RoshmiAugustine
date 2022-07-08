// -----------------------------------------------------------------------
// <copyright file="NotificationTemplate.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class NotificationTemplate : BaseEntity
    {
        public int NotificationTemplateID { get; set; }
        public int NotificationLevelID { get; set; }
        public int NotificationModeID { get; set; }
        public string Description { get; set; }
        public string TemplateText { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public NotificationLevel NotificationLevel { get; set; }
        public NotificationMode NotificationMode { get; set; }
    }
}
