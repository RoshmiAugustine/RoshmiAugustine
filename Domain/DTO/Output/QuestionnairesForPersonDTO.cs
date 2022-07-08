// -----------------------------------------------------------------------
// <copyright file="QuestionnairesForPersonDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnairesForPersonDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<PersonQuestionnaireDataDTO> PersonQuestionnaireDataDTO { get; set; }
    }
}
