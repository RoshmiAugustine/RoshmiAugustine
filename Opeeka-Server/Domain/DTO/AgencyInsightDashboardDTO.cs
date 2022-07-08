using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class AgencyInsightDashboardDTO
    {
        public int AgencyInsightDashboardId { get; set; }
        public int DashboardId { get; set; }
        public long AgencyId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string IconURL { get; set; }
        public int ListOrder { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
