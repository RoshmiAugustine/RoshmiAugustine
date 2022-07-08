// -----------------------------------------------------------------------
// <copyright file="IAssessmentEmailLinkRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAssessmentEmailLinkRepository
    /// </summary>
    public interface IAssessmentEmailLinkRepository : IAsyncRepository<AssessmentEmailLinkDetails>
    {
        /// <summary>
        /// To add AssessmentEmailLink details.
        /// </summary>
        /// <param name="addressDTO">id.</param>
        /// <returns>Guid.</returns>
        AssessmentEmailLinkDetails AddEmailLinkData(AssessmentEmailLinkDetails addressDTO);

        /// <summary>
        /// To get Get AssessmentEmailLink details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>address details..</returns>
        AssessmentEmailLinkDetails GetEmailLinkData(Guid emailLinkIndex);

        /// <summary>
        /// GetEmailLinkDataByPersonIndex.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>AssessmentEmailLinkDetails.</returns>
        AssessmentEmailLinkDetails GetEmailLinkDataByPersonIndex(Guid personIndex,int assessmentID);

        /// <summary>
        /// UpdateEmailLinkData.
        /// </summary>
        /// <param name="emailParameterDetails">emailParameterDetails.</param>
        /// <returns>AssessmentEmailLinkDetails.</returns>
        AssessmentEmailLinkDetails UpdateEmailLinkData(AssessmentEmailLinkDetails emailParameterDetails);
        List<AssessmentEmailLinkDetails> AddBulkAssessmentEmailLinkDetails(List<AssessmentEmailLinkDetails> assessmentsEmailLinkDetails);
        List<AssessmentEmailLinkDetailsDTO> GetEmailLinkDataByGuid(List<Guid?> list_assessmentEmailLinkGUID);
    }
}
