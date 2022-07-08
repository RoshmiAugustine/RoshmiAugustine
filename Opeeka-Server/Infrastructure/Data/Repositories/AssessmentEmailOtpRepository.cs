// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailOtpRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentEmailOtpRepository : BaseRepository<AssessmentEmailOtp>, IAssessmentEmailOtpRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentEmailOtpRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public AssessmentEmailOtpRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Add Assessment Email Otp Data
        /// </summary>
        /// <param name="assessmentEmailOtpData"></param>
        /// <returns></returns>
        public AssessmentEmailOtp AddAssessmentEmailOtpData(AssessmentEmailOtpDTO assessmentEmailOtpData)
        {
            try
            {
                AssessmentEmailOtp assessmentEmailOtp = new AssessmentEmailOtp();
                this.mapper.Map<AssessmentEmailOtpDTO, AssessmentEmailOtp>(assessmentEmailOtpData, assessmentEmailOtp);
                var result = this.AddAsync(assessmentEmailOtp).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Email Otp
        /// </summary>
        /// <param name="assessmentEmailOtpData"></param>
        /// <returns></returns>
        public AssessmentEmailOtp UpdateEmailOtp(AssessmentEmailOtpDTO assessmentEmailOtpData)
        {
            try
            {
                AssessmentEmailOtp otpData = new AssessmentEmailOtp();
                this.mapper.Map<AssessmentEmailOtpDTO, AssessmentEmailOtp>(assessmentEmailOtpData, otpData);
                return this.UpdateAsync(otpData).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Find EmailOtp By EmailLink
        /// </summary>
        /// <param name="AssessmentEmailLinkDetailsID"></param>
        /// <returns></returns>
        public async Task<AssessmentEmailOtp> FindEmailOtpByEmailLink(int AssessmentEmailLinkDetailsID)
        {
            try
            {
                AssessmentEmailOtpDTO assessmentEmailOtpDTO = new AssessmentEmailOtpDTO();
                AssessmentEmailOtp assessmentEmailOtp = await this.GetRowAsync(x => x.AssessmentEmailLinkDetailsID == AssessmentEmailLinkDetailsID);
                this.mapper.Map<AssessmentEmailOtp, AssessmentEmailOtpDTO>(assessmentEmailOtp, assessmentEmailOtpDTO);

                return assessmentEmailOtp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Find Is Email Otp Valid
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="assessmentEmailLinkDetailsID"></param>
        /// <returns>Task<AssessmentEmailOtp></returns>
        public async Task<AssessmentEmailOtp> FindIsEmailOtpValid(string otp, int assessmentEmailLinkDetailsID)
        {
            try
            {
                AssessmentEmailOtpDTO assessmentEmailOtpDTO = new AssessmentEmailOtpDTO();
                AssessmentEmailOtp assessmentEmailOtp = await this.GetRowAsync(x => x.Otp == otp && x.AssessmentEmailLinkDetailsID == assessmentEmailLinkDetailsID);
                this.mapper.Map<AssessmentEmailOtp, AssessmentEmailOtpDTO>(assessmentEmailOtp, assessmentEmailOtpDTO);

                return assessmentEmailOtp;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
