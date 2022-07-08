// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskScheduleDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireNotifyRiskScheduleDTO
    {
        public int QuestionnaireNotifyRiskScheduleID { get; set; }
        public string Name { get; set; }
        public int UpdateUserID { get; set; }
        public int QuestionnaireID { get; set; }
        public bool IsAlertsHelpersManagers { get; set; }
        public List<QuestionnaireNotifyRiskRuleDTO> QuestionnaireNotifyRiskRuleDTO { get; set; }

    }
}
