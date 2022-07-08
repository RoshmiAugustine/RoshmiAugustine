// -----------------------------------------------------------------------
// <copyright file="AssessmentReasonConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AssessmentReasonConfiguration : IEntityTypeConfiguration<AssessmentReason>
    {
        public void Configure(EntityTypeBuilder<AssessmentReason> builder)
        {
            builder.ToTable("AssessmentReason", "info");

            builder.Property(ci => ci.AssessmentReasonID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.UpdateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}