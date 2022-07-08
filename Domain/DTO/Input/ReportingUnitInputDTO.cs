// -----------------------------------------------------------------------
// <copyright file="ReportingUnitInputDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class ReportingUnitInputDTO
    {
        [Required]
        public string Name { get; set; }

        public string Abbrev { get; set; }

        public long ParentAgencyID { get; set; }

        [Required]
        [Range(1, Int64.MaxValue, ErrorMessage = "Please enter a valid UpdateUserID")]
        public int UpdateUserID { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }
        public bool IsSharing { get; set; }
    }
}
