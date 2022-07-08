// -----------------------------------------------------------------------
// <copyright file="PersonStrengthReportResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PersonStrengthReportResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public PersonStrengthReportDTO PersonStrengthReport { get; set; }

    }
}
