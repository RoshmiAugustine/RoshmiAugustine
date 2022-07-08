// -----------------------------------------------------------------------
// <copyright file="AgencySharing.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class AgencySharing : BaseEntity
    {
        public int AgencySharingID { get; set; }

        public Guid AgencySharingIndex { get; set; }

        public int ReportingUnitID { get; set; }

        public long AgencyID { get; set; }

        public int? AgencySharingPolicyID { get; set; }

        public bool HistoricalView { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsSharing { get; set; }
    }
}
