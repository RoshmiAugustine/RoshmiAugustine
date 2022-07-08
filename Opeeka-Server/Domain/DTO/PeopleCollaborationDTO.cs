// -----------------------------------------------------------------------
// <copyright file="PeopleCollaborationDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PeopleCollaborationDTO
    {
        public int CollaborationID { get; set; }
        public string CollaborationName { get; set; }
        public DateTime CollaborationStartDate { get; set; }
        public DateTime? CollaborationEndDate { get; set; }
        public long PersonCollaborationID { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsCurrent { get; set; }
        public int WindowOpenOffsetDays { get; set; }
        public int WindowCloseOffsetDays { get; set; }
        public DateTime EnrollDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long PersonQuestionnaireID { get; set; }
    }
}
