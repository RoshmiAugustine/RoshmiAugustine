// -----------------------------------------------------------------------
// <copyright file="AssessedQuestionnairesForPersonDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AssessedQuestionnairesForPersonDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }
        public string SuperStoryAssessmentIDs { get; set; }
        public List<AssessmentQuestionnaireDataDTO> QuestionnaireData { get; set; }
    }
}
