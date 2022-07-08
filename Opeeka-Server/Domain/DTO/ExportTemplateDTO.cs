// -----------------------------------------------------------------------
// <copyright file="ExportTemplateDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class ExportTemplateDTO
    {
        public int ExportTemplateID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string TemplateSourceText { get; set; }
        public string TemplateType { get; set; }
        public int ListOrder { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
