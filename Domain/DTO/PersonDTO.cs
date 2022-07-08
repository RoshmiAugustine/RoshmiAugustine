// -----------------------------------------------------------------------
// <copyright file="PersonDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonDTO
    {
        public long PersonID { get; set; }

        public Guid PersonIndex { get; set; }

        public string Name { get; set; }

        public string Collaboration { get; set; }

        public string Lead { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CollaborationID { get; set; }

        public int? Days { get; set; }
        public int? Assessed { get; set; }
        public int NeedsEver { get; set; }
        public int NeedsAddressing { get; set; }

        public int StrengthEver { get; set; }

        public int StrengthBuilding { get; set; }
        public int TotalCount { get; set; }
        public DateTime PersonStartDate { get; set; }
        public DateTime? PersonEndDate { get; set; }
        public bool IsRemoved { get; set; }

        public bool IsShared { get; set; }
        public long ReceivingAgencyId { get; set; }
        public long ServingAgencyId { get; set; }
        public bool IsActive { get; set; }
        public string AgencyName { get; set; }

    }
}
