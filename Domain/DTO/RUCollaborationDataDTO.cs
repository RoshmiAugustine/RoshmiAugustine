// -----------------------------------------------------------------------
// <copyright file="RUCollaborationDataDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class RUCollaborationDataDTO
    {
        public int CollaborationSharingID { get; set; }

        public Guid CollaborationSharingIndex { get; set; }

        public int CollaborationSharingPolicyID { get; set; }

        public bool HistoricalView { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        public int CollaborationID { get; set; }

        public string Collaboration { get; set; }

        public string AccessName { get; set; }

        public long AgencyID { get; set; }

        public string Agency { get; set; }
        public int SharingPolicy { get; set; }
        public bool IsSharing { get; set; }
        public List<QuestionnaireDTO> ListCollaborationQuestionaire { get; set; }
    }
}
