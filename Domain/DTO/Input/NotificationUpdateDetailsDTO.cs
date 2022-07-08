// -----------------------------------------------------------------------
// <copyright file="NotificationUpdateDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class NotificationUpdateDetailsDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid NotificationLogID")]
        public int NotificationLogID { get; set; }

        [Required]
        public DateTime NotificationDate { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a valid PersonID")]
        public long PersonID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid NotificationTypeID")]
        public int NotificationTypeID { get; set; }

        public int? FKeyValue { get; set; }

        public string NotificationData { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid NotificationResolutionStatusID")]
        public int NotificationResolutionStatusID { get; set; }

        public DateTime? StatusDate { get; set; }

        [Required]
        public bool IsRemoved { get; set; }

        public DateTime UpdateDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid UpdateUserID")]
        public int UpdateUserID { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
