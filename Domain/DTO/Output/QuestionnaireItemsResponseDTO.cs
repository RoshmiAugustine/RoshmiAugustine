// -----------------------------------------------------------------------
// <copyright file="QuestionnaireItemsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnaireItemsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireItemDTO> QuestionnaireItems { get; set; }
        public List<QuestionnaireItemsDTO> QuestionnaireItemsForDashBoard { get; set; }

    }
}
