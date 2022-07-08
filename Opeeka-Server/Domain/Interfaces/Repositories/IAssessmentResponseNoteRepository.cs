// -----------------------------------------------------------------------
// <copyright file="IAssessmentResponseNoteRepository.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAssessmentResponseNoteRepository
    {
        /// <summary>
        /// AddAssessmentResponseNote
        /// </summary>
        /// <param name="assessmentResponseNote"></param>
        /// <returns>AssessmentResponseNote</returns>
        AssessmentResponseNote AddAssessmentResponseNote(AssessmentResponseNote assessmentResponseNote);
        void AddBulkAssessmentResponseNote(List<AssessmentResponseNote> assessmentResponseNote);
    }

    public interface IAssessmentResponseAttachmentRepository
    {
        /// <summary>
        /// AddAssessmentResponseFiles
        /// </summary>
        /// <param name="assessmentResponseFile"></param>
        /// <returns>AssessmentResponseFile</returns>
        AssessmentResponseAttachment AddAssessmentResponseFile(AssessmentResponseAttachment assessmentResponseFile);
        void AddBulkAssessmentResponseFile(List<AssessmentResponseAttachment> assessmentResponseFile);
        List<AssessmentResponseAttachment> GetAllFileByAssessmentResponseId(int assessmentResponseId);
        void UpdateBulkAssessmentResponseFile(List<AssessmentResponseAttachment> assessmentResponseFile);
    }
}
