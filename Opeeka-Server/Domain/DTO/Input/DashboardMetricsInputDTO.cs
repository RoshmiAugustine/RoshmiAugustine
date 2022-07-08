using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class DashboardMetricsInputDTO
    {
        public long personId { get; set; }

        public List<int> itemIds { get; set; }
        public int AssessmentID { get; set; }
    }
}
