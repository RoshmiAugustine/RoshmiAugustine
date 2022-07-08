// -----------------------------------------------------------------------
// <copyright file="AgencyLookupDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class AgencyLookupDTO
    {
        public long AgencyID { get; set; }
        public Guid AgencyIndex { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
    }
}
