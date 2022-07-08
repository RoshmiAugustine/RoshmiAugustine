using System;
using System.Collections.Generic;
using System.Text;

namespace InviteToCompleteTriggerProcess.DTO
{
    public class BackgroundProcessLogDTO
    {
        public int BackgroundProcessLogID { get; set; }

        public string ProcessName { get; set; }

        public DateTime LastProcessedDate { get; set; }
    }
    public class BackgroundProcessResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public BackgroundProcessLogDTO BackgroundProcess { get; set; }
    }
    public class BackgroundProcessResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public BackgroundProcessResponse result { get; set; }
    }
}
