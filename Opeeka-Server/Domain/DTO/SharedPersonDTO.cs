// -----------------------------------------------------------------------
// <copyright file="SharedPersonDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class SharedPersonSearchDTO 
    {
        public Guid PersonIndex { get; set; }
        public long PersonID { get; set; }
        public long LoggedInAgencyID { get; set; }
        public int CollaborationID { get; set; }
        public int QuestionnaireID { get; set; }
        public int VoiceTypeID { get; set; }
        public long PersonCollaborationID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public long VoiceTypeFKID { get; set; }
        public string QueryType { get; set; }
        public int TopRowCount { get; set; }
        public int UserID { get; set; }
        public List<string> HelpercollaborationIDs { get; set; }
    }
}
