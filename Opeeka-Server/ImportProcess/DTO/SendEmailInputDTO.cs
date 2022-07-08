using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class ImportEmailInputDTO
    {
        public int HelperUserID { get; set; }
        public string Message { get; set; }
        public int RowNo { get; set; }
        public int ImportFileID { get; set; }
        public string ImportFileType { get; set; }
        public long AgencyID { get; set; }
        public bool IsProcessed { get; set; }
        public string ImportFileName { get; set; }
    }
}
