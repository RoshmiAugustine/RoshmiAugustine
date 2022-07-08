// -----------------------------------------------------------------------
// <copyright file="PersonRaceEthnicity.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonRaceEthnicity : BaseEntity
    {
        public long PersonRaceEthnicityID { get; set; }
        public long PersonID { get; set; }
        public int RaceEthnicityID { get; set; }
        public bool IsRemoved { get; set; }

        public Person Person { get; set; }
        public RaceEthnicity RaceEthnicity { get; set; }
    }
}
