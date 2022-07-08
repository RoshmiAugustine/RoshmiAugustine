// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailOtpConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class AssessmentEmailOtpConfiguration : IEntityTypeConfiguration<AssessmentEmailOtp>
    {
        public void Configure(EntityTypeBuilder<AssessmentEmailOtp> builder)
        {
            builder.ToTable("AssessmentEmailOtp");

            builder.HasKey(e => e.AssessmentEmailOtpID)
                    .HasName("AssessmentEmailOtpID");

            builder.Property(ci => ci.AssessmentEmailOtpID)
               .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");
        }
    }
}
