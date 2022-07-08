// -----------------------------------------------------------------------
// <copyright file="AgencySharingHistory.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class AgencySharingHistory : BaseEntity
    {
        public int AgencySharingHistoryID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool HistoricalView { get; set; }
        public int? RemovedUserID { get; set; }
        public int? RemovedNoteID { get; set; }
        public int AgencySharingID { get; set; }

        public AgencySharing AgencySharing { get; set; }
    }
}
