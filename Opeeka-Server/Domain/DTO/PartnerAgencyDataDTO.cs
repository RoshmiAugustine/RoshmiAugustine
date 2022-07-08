// -----------------------------------------------------------------------
// <copyright file="PartnerAgencyDataDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PartnerAgencyDataDTO
    {
        public int AgencySharingID { get; set; }

        public int ReportingUnitID { get; set; }

        public long AgencyID { get; set; }

        public string Agency { get; set; }

        public int AgencySharingPolicyID { get; set; }

        public bool HistoricalView { get; set; }

        public string AccessName { get; set; }

        public int SharingPolicyID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public Guid AgencySharingIndex { get; set; }
        public bool IsSharing { get; set; }
    }
}
