// -----------------------------------------------------------------------
// <copyright file="CollaborationLeadHistory.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class CollaborationLeadHistory : BaseEntity
    {
        public int CollaborationLeadHistoryID { get; set; }

        public int CollaborationID { get; set; }

        public int? LeadUserID { get; set; }

        public int? RemovedUserID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }

        public Collaboration Collaboration { get; set; }
    }
}
