// -----------------------------------------------------------------------
// <copyright file="NotificationLevelDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class NotificationLevelDTO
    {
        public int NotificationLevelID { get; set; }
        public int NotificationTypeID { get; set; }
        public string NotificationType { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public string Abbrev { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, Int64.MaxValue, ErrorMessage = "Please enter a valid UpdateUserID")]
        public int UpdateUserID { get; set; }
        public bool IsRemoved { get; set; }
        public bool RequireResolution { get; set; }
        public DateTime UpdateDate { get; set; }
        public Int64 AgencyID { get; set; }
    }
}
