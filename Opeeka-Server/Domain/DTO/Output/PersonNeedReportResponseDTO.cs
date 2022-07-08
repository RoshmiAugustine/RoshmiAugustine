// -----------------------------------------------------------------------
// <copyright file="PersonNeedReportResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PersonNeedReportResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public PersonNeedsReportDTO PersonNeedsReport { get; set; }
    }
}
