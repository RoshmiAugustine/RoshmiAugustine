// -----------------------------------------------------------------------
// <copyright file="AssessmentNoteRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentNoteRepository : BaseRepository<AssessmentNote>, IAssessmentNoteRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<AssessmentNoteRepository> logger;
        private readonly OpeekaDBContext _dbContext;
        public AssessmentNoteRepository(ILogger<AssessmentNoteRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
         : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddAssessmentNote.
        /// </summary>
        /// <param name="assessmentNote">assessmentNote.</param>
        /// <returns>AssessmentNote.</returns>
        public AssessmentNote AddAssessmentNote(AssessmentNote assessmentNote)
        {
            try
            {
                var result = this.AddAsync(assessmentNote).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}