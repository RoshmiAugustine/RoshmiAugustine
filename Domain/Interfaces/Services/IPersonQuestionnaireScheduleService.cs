// -----------------------------------------------------------------------
// <copyright file="IPersonQuestionnaireScheduleService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// IPersonQuestionnaireMetricsService.
    /// </summary>
    public interface IPersonQuestionnaireScheduleService
    {
        bool UpdatePersonQuestionnaireSchedule(PersonQuestionnaireSchedule personQuestionnaireSchedule);
        bool UpdateBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireSchedule> personQuestionnaireScheduleList);
        long AddPersonQuestionnaireSchedule(PersonQuestionnaireSchedule personQuestionnaireSchedule);
    }
}
