// -----------------------------------------------------------------------
// <copyright file="Collaboration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Collaboration : BaseEntity
    {
        public int CollaborationID { get; set; }
        public Guid CollaborationIndex { get; set; }
        public int ReportingUnitID { get; set; }
        public int TherapyTypeID { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int IntervalDays { get; set; }
        public bool IsRemoved { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? Abbreviation { get; set; }
        public long AgencyID { get; set; }
        public int? CollaborationLeadUserID { get; set; }
        public int CollaborationLevelID { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }

        public Agency Agency { get; set; }
        public CollaborationLevel CollaborationLevel { get; set; }
        public TherapyType TherapyType { get; set; }
        public User UpdateUser { get; set; }

    }
}
