// -----------------------------------------------------------------------
// <copyright file="InstrumentAgency.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class InstrumentAgency : BaseEntity
    {
        public int InstrumentAgencyID { get; set; }
        public int InstrumentID { get; set; }
        public long AgencyID { get; set; }

        public Agency Agency { get; set; }
        public Instrument Instrument { get; set; }
    }
}
