// -----------------------------------------------------------------------
// <copyright file="ExportImportResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class ExportImportResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<ExportTemplateDTO> ExportTemplate { get; set; }
    }
}
