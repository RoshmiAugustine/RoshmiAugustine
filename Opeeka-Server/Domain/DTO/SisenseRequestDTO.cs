// -----------------------------------------------------------------------
// <copyright file="SisenseRequestDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Opeeka.PICS.Domain.DTO
{
    public class SisenseRequestDTO
    {
        [JsonProperty("dashboard", NullValueHandling = NullValueHandling.Ignore)]
        public int Dashboard { get; set; }


        [JsonProperty("chart", NullValueHandling = NullValueHandling.Ignore)]
        public string Chart { get; set; }


        [JsonProperty("embed", NullValueHandling = NullValueHandling.Ignore)]
        public string PublishedVersion { get; set; }


        [JsonProperty("daterange", NullValueHandling = NullValueHandling.Ignore)]
        public string DaterangeFilter { get; set; }


        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public List<SisenseCustomFilter> AdditionalCustomFilters { get; set; }


        [JsonProperty("aggregation", NullValueHandling = NullValueHandling.Ignore)]
        public string AggregationFilter { get; set; }

        [JsonProperty("visible", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> VisibleFilters { get; set; }


        [JsonProperty("border", NullValueHandling = NullValueHandling.Ignore)]
        public string ChartBorders { get; set; }


        [JsonProperty("data_ts", NullValueHandling = NullValueHandling.Ignore)]
        public int DataLatency { get; set; }


        [JsonProperty("expires_at", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpirationDate { get; set; }

        [JsonProperty("maintain_sessions_after_expiration", NullValueHandling = NullValueHandling.Ignore)]
        public bool MaintainSessionAfterExpiration { get; set; }
    }

    public class SisenseCustomFilter
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
}