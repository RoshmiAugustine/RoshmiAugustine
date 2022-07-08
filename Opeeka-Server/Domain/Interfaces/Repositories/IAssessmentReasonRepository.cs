// -----------------------------------------------------------------------
// <copyright file="IAssessmentReasonRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAssessmentReasonRepository : IAsyncRepository<AssessmentReason>
    {
        /// <summary>
        /// GetAllAssessmentReason
        /// </summary>
        /// <returns>AssessmentReason</returns>
        List<AssessmentReason> GetAllAssessmentReason();
    }
}
