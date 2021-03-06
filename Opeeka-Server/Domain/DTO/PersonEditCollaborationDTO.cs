// -----------------------------------------------------------------------
// <copyright file="PersonEditCollaborationDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonEditCollaborationDTO
    {

        public long PersonCollaborationID { get; set; }

        public long PersonID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Collaboration ID")]
        public int CollaborationID { get; set; }

        [Required]
        public DateTime EnrollDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public bool IsPrimary { get; set; }

        [Required]
        public bool IsCurrent { get; set; }


        public bool IsRemoved { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enterUpdateUserID")]
        public int UpdateUserID { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }
    }
}
