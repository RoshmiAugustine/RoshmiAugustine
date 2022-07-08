using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class ExportTemplateReponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public byte[]   ExportByteArray { get; set; }
        public ExportTemplateDTO ExportTemplate { get; set; }

    }
}
