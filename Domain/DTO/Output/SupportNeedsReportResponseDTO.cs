// -----------------------------------------------------------------------
// <copyright file="SupportNeedsReportResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class SupportNeedsReportResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public SupportNeedsReportDTO SupportNeedsReport { get; set; }
    }
}
