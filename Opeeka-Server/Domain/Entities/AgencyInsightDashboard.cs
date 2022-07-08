using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    public class AgencyInsightDashboard : BaseEntity
    {
        public int AgencyInsightDashboardId { get; set; }
        public int DashboardId { get; set; }
        public long AgencyId { get; set; }
        public string Name { get; set; }
        public string Filters { get; set; }
        public string CustomFilters { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string IconURL { get; set; }
        public bool IsRemoved { get; set; }
        public int ListOrder { get; set; }
        public DateTime UpdateDate { get; set; }

        public Agency Agency { get; set; }
    }
}
