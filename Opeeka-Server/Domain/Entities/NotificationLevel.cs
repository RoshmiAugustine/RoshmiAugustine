// -----------------------------------------------------------------------
// <copyright file="NotificationLevel.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class NotificationLevel : BaseEntity
    {
        public int NotificationLevelID { get; set; }
        public int NotificationTypeID { get; set; }
        public string Name { get; set; }
        public string? Abbrev { get; set; }
        public string? Description { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public bool RequireResolution { get; set; }
        public long AgencyID { get; set; }

        public Agency Agency { get; set; }
        public NotificationType NotificationType { get; set; }
        public User UpdateUser { get; set; }
    }
}
