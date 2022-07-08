// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseNoteConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AssessmentResponseNoteConfiguration : IEntityTypeConfiguration<AssessmentResponseNote>
    {
        public void Configure(EntityTypeBuilder<AssessmentResponseNote> builder)
        {
            builder.ToTable("AssessmentResponseNote");

            builder.Property(ci => ci.AssessmentResponseNoteID)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.AssessmentResponse)
              .WithMany()
              .HasForeignKey(d => d.AssessmentResponseID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Note)
              .WithMany()
              .HasForeignKey(d => d.NoteID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}