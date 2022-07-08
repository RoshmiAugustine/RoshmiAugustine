// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AssessmentResponseConfiguration : IEntityTypeConfiguration<AssessmentResponse>
    {
        public void Configure(EntityTypeBuilder<AssessmentResponse> builder)
        {
            builder.ToTable("AssessmentResponse");

            builder.Property(ci => ci.AssessmentResponseID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.IsCloned);

            builder.HasOne(s => s.Assessment)
              .WithMany()
              .HasForeignKey(d => d.AssessmentID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.PersonSupport)
              .WithMany()
              .HasForeignKey(d => d.PersonSupportID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Response)
              .WithMany()
              .HasForeignKey(d => d.ResponseID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.ItemResponseBehavior)
              .WithMany()
              .HasForeignKey(d => d.ItemResponseBehaviorID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.UpdateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.QuestionnaireItem)
              .WithMany()
              .HasForeignKey(d => d.QuestionnaireItemID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.CaregiverCategory)
             .HasMaxLength(50);

            builder.Property(ci => ci.Priority);
        }
    }
}