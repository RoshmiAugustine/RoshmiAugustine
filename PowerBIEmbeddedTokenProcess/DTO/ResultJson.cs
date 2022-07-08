using System;
using System.Collections.Generic;
using System.Text;

namespace PowerBIEmbeddedTokenAPI.DTO
{
    public class ResultJson
    {
        public string EmbedToken { get; set; }
        public string EmbedUrl { get; set; }
        public Guid ReportId { get; set; }
    }
    public class ErrorResultJson
    {
        public string ErrorMessage { get; set; }
    }
}
