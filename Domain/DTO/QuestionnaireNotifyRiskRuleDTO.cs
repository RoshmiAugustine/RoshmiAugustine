// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireNotifyRiskRuleDTO
    {
        public int QuestionnaireNotifyRiskRuleID { get; set; }
        public int QuestionnaireNotifyRiskScheduleID { get; set; }
        public string Name { get; set; }
        public int NotificationLevelID { get; set; }
        public int UpdateUserID { get; set; }

        public List<QuestionnaireNotifyRiskRuleConditionDTO> QuestionnaireNotifyRiskRuleConditionDTO { get; set; }
    }
}
