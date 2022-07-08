// -----------------------------------------------------------------------
// <copyright file="DashboardStrengthMetricsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class DashboardStrengthMetricsDTO
    {
        public long Top { get; set; }
        public int ItemID { get; set; }
        public int InstrumentID { get; set; }
        public string Item { get; set; }
        public string Instrument { get; set; }
        public int Helping { get; set; }
        public int Improved { get; set; }
        public string Instrument_title { get; set; }
    }
}
