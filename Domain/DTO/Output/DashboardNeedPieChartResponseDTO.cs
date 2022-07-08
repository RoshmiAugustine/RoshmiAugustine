// -----------------------------------------------------------------------
// <copyright file="DashboardNeedPieChartResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class DashboardNeedPieChartResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public DashboardNeedPieChartDTO DashboardNeedPieChartData { get; set; }
    }
}
