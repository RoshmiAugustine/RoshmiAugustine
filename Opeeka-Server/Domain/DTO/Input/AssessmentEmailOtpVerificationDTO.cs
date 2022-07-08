// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailOtpVerificationDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class AssessmentEmailOtpVerificationDTO
    {
        [Required(ErrorMessage = "OTP is required")]
        public string otp { get; set; }

        [Required(ErrorMessage = "OTP is required")]
        public string decryptUrl { get; set; }
    }
}
