// -----------------------------------------------------------------------
// <copyright file="ReportInputDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class ReportInputDTO
    {
        public Guid PersonIndex { get; set; }
        public int CollaborationId { get; set; }
        public int QuestionnaireId { get; set; }
        public int VoiceTypeID { get; set; }
        public int AssessmentID { get; set; }
        public long PersonCollaborationID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public long VoiceTypeFKID { get; set; }
    }
    public class SuperStoryInputDTO
    {
        public Guid PersonIndex { get; set; }
        public long PersonID { get; set; }
        public int CollaborationID { get; set; }
        public string AssessmentIDs { get; set; }
        public string QuestionnairesIDs { get; set; }
    }
    public class PowerBiInputDTO
    {
        public int AgencyPowerBIReportId { get; set; }
        public string Filters { get; set; }
        public Guid PersonIndex { get; set; }
        public int CollaborationId { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public int QuestionnaireId { get; set; }
        public int VoiceTypeId { get; set; }
        public int VoiceTypeFKId { get; set; }
    }
    public class PowerBiFilterDTO : PowerBiInputDTO
    {
        public string CollaborationName { get; set; }
        public long PersonId { get; set; }
        public string VoiceTypeName { get; set; }
        public string QuestionnaireName { get; set; }
    }
}
