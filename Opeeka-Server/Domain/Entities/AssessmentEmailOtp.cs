// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailOtp.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class AssessmentEmailOtp : BaseEntity
    {
        public int AssessmentEmailOtpID { get; set; }
        public int AssessmentEmailLinkDetailsID { get; set; }
        public string Otp { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime UpdateDate { get; set; }
        public AssessmentEmailLinkDetails AssessmentEmailLinkDetails { get; set; }
    }
}
