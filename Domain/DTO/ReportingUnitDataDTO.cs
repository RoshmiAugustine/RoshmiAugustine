// -----------------------------------------------------------------------
// <copyright file="ReportingUnitDataDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class ReportingUnitDataDTO
    {
        public int totalCount;

        public int ReportingUnitID { get; set; }
        public Guid ReportingUnitIndex { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public long AgencyID { get; set; }
        public string Agency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public bool IsSharing { get; set; }
    }
}