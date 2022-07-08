// -----------------------------------------------------------------------
// <copyright file="AssessmentNoteConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AssessmentNoteConfiguration : IEntityTypeConfiguration<AssessmentNote>
    {
        public void Configure(EntityTypeBuilder<AssessmentNote> builder)
        {
            builder.ToTable("AssessmentNote");

            builder.Property(ci => ci.AssessmentNoteID)
               .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Assessment)
                   .WithMany()
                   .HasForeignKey(d => d.AssessmentID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.AssessmentReviewHistory)
                .WithMany()
                .HasForeignKey(d => d.AssessmentReviewHistoryID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Note)
                .WithMany()
                .HasForeignKey(d => d.NoteID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
