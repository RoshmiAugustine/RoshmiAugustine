using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    public class AgencyPowerBIReport : BaseEntity
    {
        public int AgencyPowerBIReportId { get; set; }
        public long AgencyId { get; set; }
        public int InstrumentId { get; set; }
        public string ReportName { get; set; }
        public Guid ReportId { get; set; }
        public string FiltersOrParameters { get; set; }
        public bool IsRemoved { get; set; }
        public int ListOrder { get; set; }
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// If True FiltersOrParameters expect the parameter JSON.
        /// If False FiltersOrParameters expect the filter JSON.
        /// </summary>
        public bool IsRDLReport { get; set; }

        public Agency Agency { get; set; }
        public Instrument Instrument { get; set; }
    }
}
