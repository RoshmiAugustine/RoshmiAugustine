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

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentResponseNoteRepository : BaseRepository<AssessmentResponseNote>, IAssessmentResponseNoteRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentResponseNoteRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public AssessmentResponseNoteRepository(OpeekaDBContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// AddAssessmentResponseNote
        /// </summary>
        /// <param name="assessmentResponseNote"></param>
        /// <returns>AssessmentResponseNote</returns>
        public AssessmentResponseNote AddAssessmentResponseNote(AssessmentResponseNote assessmentResponseNote)
        {
            try
            {
                var result = this.AddAsync(assessmentResponseNote).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddBulkAssessmentResponseNote(List<AssessmentResponseNote> assessmentResponseNote)
        {
            try
            {
                var result = this.AddBulkAsync(assessmentResponseNote);
                result.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
