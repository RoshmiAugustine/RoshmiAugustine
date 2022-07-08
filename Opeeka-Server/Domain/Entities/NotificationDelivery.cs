// -----------------------------------------------------------------------
// <copyright file="NotificationDelivery.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class NotificationDelivery : BaseEntity
    {
        public int NotificationDeliveryID { get; set; }
        public int NotificationLogID { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int NotificationTemplateID { get; set; }

        public NotificationLog NotificationLog { get; set; }
        public NotificationTemplate NotificationTemplate { get; set; }
    }
}
