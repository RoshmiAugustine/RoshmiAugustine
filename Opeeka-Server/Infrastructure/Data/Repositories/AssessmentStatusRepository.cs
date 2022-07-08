// -----------------------------------------------------------------------
// <copyright file="AssessmentStatusRepository.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentStatusRepository : BaseRepository<AssessmentStatus>, IAssessmentStatusRepository
    {
        private readonly ICache _cache;
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentStatusRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public AssessmentStatusRepository(OpeekaDBContext dbContext, ICache cache)
            : base(dbContext)
        {
            this._cache = cache;
        }

        /// <summary>
        /// GetAssessmentStatus
        /// </summary>
        /// <param name="assessmentStatus"></param>
        /// <returns>AssessmentStatus</returns>
        public async Task<AssessmentStatus> GetAssessmentStatus(string assessmentStatus)
        {
            try
            {
                var readFromCache = this._cache.Get<List<AssessmentStatus>>(PCISEnum.Caching.GetAllAssessmentStatus);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var assessmentStatusResult = await this.GetRowAsync(x => x.Name == assessmentStatus);
                    return assessmentStatusResult;
                }
                return readFromCache.Where(x => x.Name == assessmentStatus).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get AssessmentStatusdetails.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AssessmentStatus.</returns>
        public async Task<AssessmentStatus> GetAssessmentStatusDetails(int id)
        {
            try
            {
                var readFromCache = this._cache.Get<List<AssessmentStatus>>(PCISEnum.Caching.GetAllAssessmentStatus);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    AssessmentStatus assessmentStatus = await this.GetRowAsync(x => x.AssessmentStatusID == id);
                    return assessmentStatus;
                }
                return readFromCache.Where(x => x.AssessmentStatusID == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAssessmentStatus
        /// </summary>
        /// <returns></returns>
        public  List<AssessmentStatus> GetAllAssessmentStatus()
        {
            try
            {
                var readFromCache = this._cache.Get<List<AssessmentStatus>>(PCISEnum.Caching.GetAllAssessmentStatus);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    List<AssessmentStatus> assessmentStatus = this.GetAllAsync().Result.ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllAssessmentStatus, readFromCache = assessmentStatus);
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
