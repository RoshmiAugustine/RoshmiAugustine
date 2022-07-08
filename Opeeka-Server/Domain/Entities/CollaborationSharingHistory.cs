// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingHistory.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class CollaborationSharingHistory : BaseEntity
    {
        public int CollaborationSharingHistoryID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool HistoricalView { get; set; }
        public int? RemovedUserID { get; set; }
        public int? RemovedNoteID { get; set; }
        public int CollaborationSharingID { get; set; }

        public CollaborationSharing CollaborationSharing { get; set; }
    }
}
