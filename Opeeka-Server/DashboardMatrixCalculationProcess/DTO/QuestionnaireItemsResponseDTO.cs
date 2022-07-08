// -----------------------------------------------------------------------
// <copyright file="QuestionnaireItemsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace DashboardMatrixCalculationProcess.DTO
{
    public class QuestionnaireItemsResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public List<QuestionnaireItemsDTO> QuestionnaireItemsForDashBoard { get; set; }

    }
    public class QuestionnaireItemsResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public QuestionnaireItemsResponse result { get; set; }
    }

}
