// -----------------------------------------------------------------------
// <copyright file="CollaborationLeadHistoryDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationLeadHistoryDTO
    {
        public int CollaborationLeadHistoryID { get; set; }

        public int CollaborationID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a collaboration lead user")]
        public int LeadUserID { get; set; }

        public int RemovedUserID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }
    }
}
