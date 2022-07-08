// -----------------------------------------------------------------------
// <copyright file="AssessmentConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.ToTable("Assessment");

            builder.Property(ci => ci.AssessmentID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.PersonQuestionnaire)
              .WithMany()
              .HasForeignKey(d => d.PersonQuestionnaireID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.VoiceType)
              .WithMany()
              .HasForeignKey(d => d.VoiceTypeID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.AssessmentReason)
              .WithMany()
              .HasForeignKey(d => d.AssessmentReasonID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.AssessmentStatus)
              .WithMany()
              .HasForeignKey(d => d.AssessmentStatusID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.PersonQuestionnaireSchedule)
              .WithMany()
              .HasForeignKey(d => d.PersonQuestionnaireScheduleID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.UpdateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.SubmittedUser)
              .WithMany()
              .HasForeignKey(d => d.SubmittedUserID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}