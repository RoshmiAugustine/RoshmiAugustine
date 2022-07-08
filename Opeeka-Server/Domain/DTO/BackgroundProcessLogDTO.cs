using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class BackgroundProcessLogDTO
    {
        public int BackgroundProcessLogID { get; set; }

        public string ProcessName { get; set; }

        public DateTime LastProcessedDate { get; set; }
    }
}
