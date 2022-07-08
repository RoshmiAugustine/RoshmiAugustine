// -----------------------------------------------------------------------
// <copyright file="ReportingUnit.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class ReportingUnit : BaseEntity
    {
        public int ReportingUnitID { get; set; }
        public Guid ReportingUnitIndex { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public long ParentAgencyID { get; set; }
        public bool IsRemoved { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Agency ParentAgency { get; set; }
        public User UpdateUser { get; set; }
        public string Description { get; set; }
        public bool IsSharing { get; set; }
    }
}
