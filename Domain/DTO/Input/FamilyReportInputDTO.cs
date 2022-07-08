// -----------------------------------------------------------------------
// <copyright file="FamilyReportInputDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class FamilyReportInputDTO
    {
        public Guid PersonIndex { get; set; }
        public long PersonCollaborationID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public int VoiceTypeID { get; set; }
        public long VoiceTypeFKID { get; set; }
    }
}
