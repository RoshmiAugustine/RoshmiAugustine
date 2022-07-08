// -----------------------------------------------------------------------
// <copyright file="CollaborationInfoDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationInfoDTO
    {
        public int CollaborationID { get; set; }

        public Guid CollaborationIndex { get; set; }

        public int TherapyTypeID { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }

        public int UpdateUserID { get; set; }

        public long AgencyID { get; set; }

        public string Agency { get; set; }

        public int CollaborationLevelID { get; set; }

        public string Level { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public List<QuestionnaireDataDTO> Questionnaire { get; set; }

        public List<CategoryDataDTO> Category { get; set; }

        public List<CollaborationLeadDTO> Lead { get; set; }
        public string Abbreviation { get; set; }
    }
}
