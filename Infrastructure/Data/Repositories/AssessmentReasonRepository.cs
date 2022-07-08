// -----------------------------------------------------------------------
// <copyright file="AssessmentReasonRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentReasonRepository : BaseRepository<AssessmentReason>, IAssessmentReasonRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentReasonRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public AssessmentReasonRepository(OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// GetAllAssessmentReason
        /// </summary>
        /// <returns>AssessmentReason</returns>
        public List<AssessmentReason> GetAllAssessmentReason()
        {
            try
            {
                var readFromCache = this._cache.Get<List<AssessmentReason>>(PCISEnum.Caching.GetAllAssessmentReason);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var assessmentReason = this.GetAsync(x => !x.IsRemoved).Result;
                    var response = assessmentReason.OrderBy(x => x.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllAssessmentReason, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
