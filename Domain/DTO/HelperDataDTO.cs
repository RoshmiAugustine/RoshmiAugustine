// -----------------------------------------------------------------------
// <copyright file="HelperDataDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class HelperDataDTO
    {
        public int HelperID { get; set; }
        public int LeadHelerID { get; set; }

        public Guid? HelperIndex { get; set; }
        public Guid? LeadHelperIndex { get; set; }

        public string HelperName { get; set; }

        public string LeadName { get; set; }

        public int Helping { get; set; }
        public int Days { get; set; }

        public int? Due { get; set; }

        public int? Completed { get; set; }
        public int NeedIdentified { get; set; }
        public int NeedAddressed { get; set; }

        public int StrengthIdentified { get; set; }
        public int StrengthBuilt { get; set; }

        public int Improved { get; set; }

        public int NextDue { get; set; }

        public int TotalCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int NeedsEver { get; set; }
        public int NeedsAddressing { get; set; }
        public int StrengthsEver { get; set; }
        public int StrengthsBuilding { get; set; }

    }
}
