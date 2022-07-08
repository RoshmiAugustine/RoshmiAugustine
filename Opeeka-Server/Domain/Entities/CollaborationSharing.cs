// -----------------------------------------------------------------------
// <copyright file="CollaborationSharing.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class CollaborationSharing : BaseEntity
    {
        public int CollaborationSharingID { get; set; }

        public Guid CollaborationSharingIndex { get; set; }

        public int ReportingUnitID { get; set; }

        public long AgencyID { get; set; }

        public int CollaborationID { get; set; }

        public int? CollaborationSharingPolicyID { get; set; }

        public bool HistoricalView { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool IsSharing { get; set; }
        public Agency Agency { get; set; }
        public Collaboration Collaboration { get; set; }
        public ReportingUnit ReportingUnit { get; set; }
    }
}
