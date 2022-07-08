// -----------------------------------------------------------------------
// <copyright file="QuestionsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionsDTO
    {
        public int QuestionnaireID { get; set; }
        public string QuestionnaireName { get; set; }
        public string Categories { get; set; }
        public string InstrumentAbbrev { get; set; }
        public string QuestionnaireAbbrev { get; set; }
        public string RequiredConfidentialityLanguage { get; set; }
        public string PersonRequestedConfidentialityLanguage { get; set; }
        public string OtherConfidentialityLanguage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Instruction { get; set; }
        public string InstrumentUrl { get; set; }
        public Boolean HasFormView { get; set; }
        public string InstrumentDescription { get; set; }
        public string QuestionnaireDescription { get; set; }

        //public List<CategoryDTO> Categories { get; set; }
    }
    public class QuestionnaireSkipActionDetailsDTO
    {
        public int QuestionnaireID { get; set; }
        public string SkippedItems { get; set; }
        public string SkippedCategories { get; set; }
        public string SkippedChildItems { get; set; }
    }
}
