// -----------------------------------------------------------------------
// <copyright file="ExportTemplateAgency.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.Entities
{
    public class ExportTemplateAgency
    {
        public int ExportTemplateAgencyID { get; set; }
        public int ExportTemplateID { get; set; }
        public long AgencyID { get; set; }


        public Agency Agency { get; set; }
        public ExportTemplate ExportTemplate { get; set; }

    }
}
