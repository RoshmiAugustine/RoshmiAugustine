// -----------------------------------------------------------------------
// <copyright file="AssessmentStatusConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AssessmentStatusConfiguration : IEntityTypeConfiguration<AssessmentStatus>
    {
        public void Configure(EntityTypeBuilder<AssessmentStatus> builder)
        {
            builder.ToTable("AssessmentStatus", "info");

            builder.Property(ci => ci.AssessmentStatusID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.updateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}