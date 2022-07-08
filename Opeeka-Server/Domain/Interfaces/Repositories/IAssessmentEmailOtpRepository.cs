// -----------------------------------------------------------------------
// <copyright file="IAssessmentEmailOtpRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAssessmentEmailOtpRepository : IAsyncRepository<AssessmentEmailOtp>
    {
        /// <summary>
        /// Add Assessment Email Otp Data
        /// </summary>
        /// <param name="assessmentEmailOtpData"></param>
        /// <returns>AssessmentEmailOtp</returns>
        AssessmentEmailOtp AddAssessmentEmailOtpData(AssessmentEmailOtpDTO assessmentEmailOtpData);

        /// <summary>
        /// UpdateEmailOtp
        /// </summary>
        /// <param name="assessmentEmailOtpData"></param>
        /// <returns>AssessmentEmailOtp</returns>
        AssessmentEmailOtp UpdateEmailOtp(AssessmentEmailOtpDTO assessmentEmailOtpData);

        /// <summary>
        /// Find Email Otp By Email Link
        /// </summary>
        /// <param name="AssessmentEmailLinkDetailsID"></param>
        /// <returns>Task<AssessmentEmailOtp></returns>
        Task<AssessmentEmailOtp> FindEmailOtpByEmailLink(int AssessmentEmailLinkDetailsID);

        /// <summary>
        /// Find Is Email Otp Valid
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="AssessmentEmailLinkDetailsID"></param>
        /// <returns>Task<AssessmentEmailOtp></returns>
        Task<AssessmentEmailOtp> FindIsEmailOtpValid(string otp, int AssessmentEmailLinkDetailsID);
    }
}
