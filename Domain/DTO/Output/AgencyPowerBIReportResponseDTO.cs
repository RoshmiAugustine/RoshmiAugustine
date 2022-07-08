using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AgencyPowerBIReportResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public List<AgencyPowerBIReportDTO> PowerBiReports { get; set; }
    }
    public class AgencyPowerBIReportDTO
    {
        public int AgencyPowerBIReportId { get; set; }
        public long AgencyId { get; set; }
        public int InstrumentId { get; set; }
        public string ReportName { get; set; }
        public Guid ReportId { get; set; }
        public string Filters { get; set; }
        public int ListOrder { get; set; }
    }
    public class PowerBIReportURLResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public PowerBiReportURLDTO PowerBiReportURLData { get; set; }
    }
    public class PowerBiReportURLDTO
    {
        public string EmbedToken { get; set; }
        public string EmbedUrl { get; set; }
        public string ReportId { get; set; }
        public string Filters { get; set; }
    }
    public class PowerBiParameterDTO
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public string @Operator { get; set; }
        public List<string> Values { get; set; }
    }
}
