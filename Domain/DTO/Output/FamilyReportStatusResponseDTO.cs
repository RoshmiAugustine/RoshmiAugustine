// -----------------------------------------------------------------------
// <copyright file="FamilyReportStatusResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.DTO.Output
{
    public class FamilyReportStatusResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public FamilyReportStatusDTO FamilyReportStatus { get; set; }
    }
}
