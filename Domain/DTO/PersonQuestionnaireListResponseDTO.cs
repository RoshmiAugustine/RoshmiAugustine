// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireListResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonQuestionnaireListResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<PersonQuestionnaireListDTO> PersonQuestionnaireListDTO { get; set; }
    }
}
