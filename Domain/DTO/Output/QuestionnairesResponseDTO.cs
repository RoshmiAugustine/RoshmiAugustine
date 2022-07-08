// -----------------------------------------------------------------------
// <copyright file="QuestionnaireResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnairesResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

       public QuestionnairesDTO Questionnaire { get; set; }
       public IReadOnlyList<QuestionnairesDTO> QuestionnaireList { get; set; }
    }
}
