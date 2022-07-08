// -----------------------------------------------------------------------
// <copyright file="FamilyReportStatusDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class FamilyReportStatusDTO
    {
        public bool PersonStrengthReportStatus { get; set; }
        public bool PersonNeedsReportStatus { get; set; }
        public bool SupportResourceReportStatus { get; set; }
        public bool SupportNeedsReportStatus { get; set; }
    }
}
