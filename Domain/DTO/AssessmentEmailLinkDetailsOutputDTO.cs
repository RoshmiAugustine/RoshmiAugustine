// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailLinkDetailsOutputDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentEmailLinkDetailsOutputDTO
    {
        public Guid PersonIndex { get; set; }
        public string PersonInitials { get; set; }
        public int? PersonSupportID { get; set; }
        public int QuestionnaireID { get; set; }
        public int? HelperID { get; set; }
        public int UpdateUserID { get; set; }

        public AssessmentDTO Assessment { get; set; }
        public List<PeopleSupportDTO> personSupports { get; set; }
    }
}
