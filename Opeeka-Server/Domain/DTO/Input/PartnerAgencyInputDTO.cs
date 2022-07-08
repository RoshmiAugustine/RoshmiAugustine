// -----------------------------------------------------------------------
// <copyright file="PartnerAgencyInputDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class PartnerAgencyInputDTO
    {
        public Guid AgencySharingIndex { get; set; }

        [Required(ErrorMessage = "Reporting Unit is required")]
        public int ReportingUnitID { get; set; }

        [Required(ErrorMessage = "Agency is required")]
        public long AgencyID { get; set; }

        [Required(ErrorMessage = "agencySharingPolicyID is required")]
        public int? AgencySharingPolicyID { get; set; }

        public bool? HistoricalView { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool IsSharing { get; set; }

    }
}
