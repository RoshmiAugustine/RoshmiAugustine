// -----------------------------------------------------------------------
// <copyright file="AssessmentHistoryRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using AutoMapper;
using Opeeka.PICS.Domain.Entities;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentHistoryRepository : BaseRepository<ReviewerHistory>, IAssessmentHistoryRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<AssessmentHistoryRepository> logger;
        private readonly OpeekaDBContext _dbContext;
        public AssessmentHistoryRepository(ILogger<AssessmentHistoryRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
         : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddAssessmentHistory.
        /// </summary>
        /// <param name="assessmentHistory">assessmentNote.</param>
        /// <returns>ReviewerHistory.</returns>
        public ReviewerHistory AddAssessmentHistory(ReviewerHistory assessmentHistory)
        {
            try
            {
                var result = this.AddAsync(assessmentHistory).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetHistoryForAssessment
        /// </summary>
        /// <param name="assessmentID">assessmentID</param>
        /// <param name="statusId">statusId</param>
        /// <returns>ReviewerHistory</returns>
        public ReviewerHistory GetHistoryForAssessment(int assessmentID, int statusId)
        {
            try
            {
                var query = string.Empty;
                query = @"select ARH.AssessmentReviewHistoryID from ReviewerHistory ARH
                        join AssessmentNote AN on AN.AssessmentReviewHistoryID=ARH.AssessmentReviewHistoryID
                        where AN.AssessmentReviewHistoryID is not null 
                        and AN.AssessmentID=" + assessmentID + @"
                        and IsRemoved=0
                        and StatusTo=" + statusId;

                var ReviewerHistory = ExecuteSqlQuery(query, x => new ReviewerHistory
                {
                    AssessmentReviewHistoryID = x[0] == DBNull.Value ? 0 : (int)x[0]

                }).FirstOrDefault();

                return ReviewerHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
