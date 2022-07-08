// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailOtpResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AssessmentEmailOtpResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public int AssessmentEmailOtpID { get; set; }
        public bool IsOTPVerificationNeeded { get; set; }
    }
}
