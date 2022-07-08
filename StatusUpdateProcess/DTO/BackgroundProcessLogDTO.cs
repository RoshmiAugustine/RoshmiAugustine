using System;
using System.Collections.Generic;
using System.Text;

namespace StatusUpdateProcess.DTO
{
    public class BackgroundProcessLogDTO
    {
        public int BackgroundProcessLogID { get; set; }

        public string ProcessName { get; set; }

        public DateTime LastProcessedDate { get; set; }
    }
}
