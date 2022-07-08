﻿// -----------------------------------------------------------------------
// <copyright file="QuestionnaireListResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnaireListResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<QuestionnaireDTO> QuestionnaireList { get; set; }
    }
}
