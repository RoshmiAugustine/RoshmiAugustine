// -----------------------------------------------------------------------
// <copyright file="DashboardStrengthPieChartResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class DashboardStrengthPieChartResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public DashboardStrengthPieChartDTO DashboardStrengthPieChartData { get; set; }
    }
}
