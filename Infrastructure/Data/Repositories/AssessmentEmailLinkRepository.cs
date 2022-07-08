// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailLinkRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentEmailLinkRepository : BaseRepository<AssessmentEmailLinkDetails>, IAssessmentEmailLinkRepository
    {
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentEmailLinkRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public AssessmentEmailLinkRepository(OpeekaDBContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public AssessmentEmailLinkDetails AddEmailLinkData(AssessmentEmailLinkDetails emailParameterDetails)
        {
            try
            {
                var result = this.AddAsync(emailParameterDetails).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AssessmentEmailLinkDetails GetEmailLinkData(Guid emailLinkIndex)
        {
            try
            {
                var result = this.GetRowAsync(x => x.EmailLinkDetailsIndex == emailLinkIndex).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetEmailLinkDataByPersonIndex.
        /// </summary>
        /// <param name="personIndex">personIndex.</param>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>AssessmentEmailLinkDetails.</returns>
        public AssessmentEmailLinkDetails GetEmailLinkDataByPersonIndex(Guid personIndex, int assessmentID)
        {
            try
            {
                var result = this.GetRowAsync(x => x.PersonIndex == personIndex && x.AssessmentID == assessmentID).Result;
                return result; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateEmailLinkData.
        /// </summary>
        /// <param name="emailParameterDetails">emailParameterDetails.</param>
        /// <returns>AssessmentEmailLinkDetails.</returns>
        public AssessmentEmailLinkDetails UpdateEmailLinkData(AssessmentEmailLinkDetails emailParameterDetails)
        {
            try
            {
                var result = this.UpdateAsync(emailParameterDetails).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddBulkAssessmentEmailLinkDetails.
        /// </summary>
        /// <param name="assessmentResponse"></param>
        /// <returns></returns>
        public List<AssessmentEmailLinkDetails> AddBulkAssessmentEmailLinkDetails(List<AssessmentEmailLinkDetails> assessmentsEmailLinkDetails)
        {
            try
            {
                var res = this.AddBulkAsync(assessmentsEmailLinkDetails);
                res.Wait();
                return assessmentsEmailLinkDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetEmailLinkDataByGUID
        /// </summary>
        /// <param name="emailLinkIndex"></param>
        /// <returns></returns>
        public List<AssessmentEmailLinkDetailsDTO> GetEmailLinkDataByGuid(List<Guid?> list_assessmentGUID)
        {
            try
            {
                var query = @$"SELECT EmailLinkDetailsIndex, AssessmentEmailLinkGuid, AssessmentID FROM AssessmentEmailLinkDetails WHERE AssessmentEmailLinkGuid in ('{string.Join("','", list_assessmentGUID)}')";

                var data = ExecuteSqlQuery(query, x => new AssessmentEmailLinkDetailsDTO
                {
                    EmailLinkDetailsIndex = (Guid)x["EmailLinkDetailsIndex"],
                    AssessmentEmailLinkGUID = (Guid?)x["AssessmentEmailLinkGuid"],
                    AssessmentID = (int)x["AssessmentID"]
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
