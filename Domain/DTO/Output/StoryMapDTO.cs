// -----------------------------------------------------------------------
// <copyright file="StoryMapDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Linq.Expressions;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class StoryMapDTO
    {
        public string Type { get; set; }
        public int ItemID { get; set; }
        public string Item { get; set; }
        public string LabelDescription { get; set; }
        public string Label { get; set; }
        public decimal? Score { get; set; }
        public Int64? Priority { get; set; }
        public string ToDo { get; set; }
        public int AssessmentResponseID { get; set; }
        public string Rgb { get; set; }
        public int ResponseValueTypeID { get; set; }
        public decimal minRange { get; set; }
        public int maxRange { get; set; }
        public string AssessmentResponseValue { get; set; }
    }
    public class SuperStoryMapDTO : StoryMapDTO
    {
        public string InstrumentAbbrev { get; set; }
        public string VoiceType { get; set; }
        public int VoiceTypeID { get; set; }
        public long? VoiceTypeFKID { get; set; }
        public string VoiceTypeInDetail { get; set; }
        public int QuestionnaireID { get; set; }
        public string QuestionnaireName { get; set; }
    }
}
