// -----------------------------------------------------------------------
// <copyright file="CollaborationDataDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationDataDTO
    {
        public int CollaborationID { get; set; }

        public Guid CollaborationIndex { get; set; }

        public int TherapyTypeID { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Code { get; set; }

        public long AgencyID { get; set; }

        public string Agency { get; set; }

        public int CollaborationLevelID { get; set; }

        public string Level { get; set; }

        public int TotalCount { get; set; }
    }
}
