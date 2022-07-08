// -----------------------------------------------------------------------
// <copyright file="CollaborationDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationDetailsDTO
    {
        public int CollaborationID { get; set; }

        public Guid CollaborationIndex { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid therapy type")]
        public int TherapyTypeID { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid update userID")]
        public int UpdateUserID { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "Please enter a valid agency")]
        public long AgencyID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid collaboration level")]
        public int CollaborationLevelID { get; set; }

        public string Code { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public List<CollaborationQuestionnaireDTO> Questionnaire { get; set; }

        public List<CollaborationTagDTO> Category { get; set; }

        public List<CollaborationLeadHistoryDTO> Lead { get; set; }
    }
}
