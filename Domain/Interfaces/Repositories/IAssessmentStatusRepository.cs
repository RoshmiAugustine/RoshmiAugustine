// -----------------------------------------------------------------------
// <copyright file="IAssessmentStatusRepository.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Graph;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAssessmentStatusRepository
    {
        /// <summary>
        /// GetAssessmentStatus
        /// </summary>
        /// <param name="assessmentStatus"></param>
        /// <returns>AssessmentStatus</returns>
        Task<AssessmentStatus> GetAssessmentStatus(string assessmentStatus);

        /// <summary>
        /// To get AssessmentStatusdetails.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AssessmentStatus.</returns>
        Task<AssessmentStatus> GetAssessmentStatusDetails(int id);

        List<AssessmentStatus> GetAllAssessmentStatus();

    }
}
