// -----------------------------------------------------------------------
// <copyright file="ReportingUnitDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class ReportingUnitDTO
    {
        public Guid ReportingUnitIndex { get; set; }
        public int ReportingUnitID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public long ParentAgencyID { get; set; }
        public bool IsRemoved { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public bool IsSharing { get; set; }
    }
}
