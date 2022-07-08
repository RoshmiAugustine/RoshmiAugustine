// -----------------------------------------------------------------------
// <copyright file="ReportingUnitDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class EditReportingUnitDTO
    {
        public Guid ReportingUnitIndex { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abbrev { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a UpdateUserID")]
        public int UpdateUserID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public bool IsSharing { get; set; }
    }
}
