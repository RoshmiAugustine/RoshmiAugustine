// -----------------------------------------------------------------------
// <copyright file="QuestionsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public QuestionsDTO Questions { get; set; }
        public QuestionnaireSkipActionDetailsDTO QuestionsSkippedActionDetails { get; set; }
    }
}
