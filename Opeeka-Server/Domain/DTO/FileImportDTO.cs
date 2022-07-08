using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class FileImportDTO
    {
        public int FileImportID { get; set; }
        public string ImportType { get; set; }
        public string FileJsonData { get; set; }
        public int UpdateUserID { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime CreatedDate { get; set; }
        public long AgencyID { get; set; }
        public int ImportTypeID { get; set; }
        public int? QuestionnaireID { get; set; }
        public string ImportFileName { get; set; }
    }
}
