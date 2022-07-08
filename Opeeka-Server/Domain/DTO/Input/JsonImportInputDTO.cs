using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class JsonImportInputDTO
    {
        public string UploadJSON { get; set; }
        public string FileName { get; set; }
        public long AgencyID { get; set; }
        public int ImportTypeID { get; set; }
        public int? QuestionnaireID { get; set; }
    }
}
