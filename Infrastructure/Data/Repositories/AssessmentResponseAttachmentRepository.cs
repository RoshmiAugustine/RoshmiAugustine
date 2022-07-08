// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseNoteRepository.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{

    public class AssessmentResponseAttachmentRepository : BaseRepository<AssessmentResponseAttachment>, IAssessmentResponseAttachmentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentResponseAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public AssessmentResponseAttachmentRepository(OpeekaDBContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// AddAssessmentResponseNote
        /// </summary>
        /// <param name="assessmentResponseNote"></param>
        /// <returns>AssessmentResponseNote</returns>
        public AssessmentResponseAttachment AddAssessmentResponseFile(AssessmentResponseAttachment assessmentResponsefile)
        {
            try
            {
                var result = this.AddAsync(assessmentResponsefile).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddBulkAssessmentResponseFile(List<AssessmentResponseAttachment> assessmentResponseFile)
        {
            try
            {
                var result = this.AddBulkAsync(assessmentResponseFile);
                result.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateBulkAssessmentResponseFile(List<AssessmentResponseAttachment> assessmentResponseFile)
        {
            try
            {
                var result = this.UpdateBulkAsync(assessmentResponseFile);
                result.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AssessmentResponseAttachment> GetAllFileByAssessmentResponseId(int assessmentResponseId)
        {
            try
            {
                var result = this.GetAsync(x => x.AssessmentResponseID == assessmentResponseId && !x.IsRemoved).Result;
                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
