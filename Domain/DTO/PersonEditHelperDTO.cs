// -----------------------------------------------------------------------
// <copyright file="PersonEditHelperDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonEditHelperDTO
    {

        public long PersonHelperID { get; set; }

        public long PersonID { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please enter a Helper ID")]
        public int HelperID { get; set; }
        [Required]
        public bool IsLead { get; set; }
        [Required]
        public bool IsCurrent { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a UpdateUserID ")]
        public int UpdateUserID { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        public int? CollaborationID { get; set; }
    }
}
