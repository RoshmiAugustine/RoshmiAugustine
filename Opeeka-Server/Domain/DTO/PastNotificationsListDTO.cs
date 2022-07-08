// -----------------------------------------------------------------------
// <copyright file="PastNotificationsListDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PastNotificationsListDTO
    {
        public int NotificationLogID { get; set; }
        public string NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public long PersonID { get; set; }
        public bool IsRemoved { get; set; }
        public int NotificationTypeID { get; set; }
        public int? FKeyValue { get; set; }
        public int NotificationResolutionStatusID { get; set; }
    }
}
