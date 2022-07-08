// -----------------------------------------------------------------------
// <copyright file="AssessmentHistoryConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ReviewerHistoryConfiguration : IEntityTypeConfiguration<ReviewerHistory>
    {
        public void Configure(EntityTypeBuilder<ReviewerHistory> builder)
        {
            builder.ToTable("ReviewerHistory");

            builder.HasKey(e => e.AssessmentReviewHistoryID)
                    .HasName("AssessmentReviewHistoryID");

            builder.Property(ci => ci.AssessmentReviewHistoryID)
               .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.StatusFromNavigation)
                 .WithMany()
                 .HasForeignKey(d => d.StatusFrom)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.StatusToNavigation)
                .WithMany()
                .HasForeignKey(d => d.StatusTo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
