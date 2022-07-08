// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskSchedulesDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireNotifyRiskSchedulesDTO
    {
        public int QuestionnaireNotifyRiskScheduleID { get; set; }

        public string Name { get; set; }

        public int QuestionnaireID { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserID { get; set; }
    }
}
