// -----------------------------------------------------------------------
// <copyright file="TimeFrame.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class TimeFrame : BaseEntity
    {
        public int TimeFrameID { get; set; }
        public int DaysInService { get; set; }
        public decimal Month_Exact { get; set; }
        public int Months_Int { get; set; }
        public decimal Qrts_Exact { get; set; }
        public int Qrts_Int { get; set; }
        public int Qrt_Current { get; set; }
        public decimal Yrs_Exact { get; set; }
        public int Years_Int { get; set; }
        public string Timeframe_Std { get; set; }
    }
}
