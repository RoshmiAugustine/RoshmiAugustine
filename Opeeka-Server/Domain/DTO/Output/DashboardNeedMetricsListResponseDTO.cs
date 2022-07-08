// -----------------------------------------------------------------------
// <copyright file="DashboardNeedMetricsListResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class DashboardNeedMetricsListResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<DashboardNeedMetricsDTO> DashboardNeedMetricsList { get; set; }
    }
}
