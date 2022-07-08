// -----------------------------------------------------------------------
// <copyright file="QuestionnaireWindowConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireWindowConfiguration : IEntityTypeConfiguration<QuestionnaireWindow>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireWindow> builder)
        {
            builder.ToTable("QuestionnaireWindow");

            builder.Property(ci => ci.QuestionnaireWindowID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.OpenOffsetTypeID)
                .HasColumnType("char(1)")
                .IsRequired(false)
                .HasMaxLength(1).HasDefaultValue('d');
            builder.Property(ci => ci.CloseOffsetTypeID)
                .HasColumnType("char(1)")
                .IsRequired(false)
                .HasMaxLength(1).HasDefaultValue('d');

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.AssessmentReason)
                  .WithMany()
                  .HasForeignKey(d => d.AssessmentReasonID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Questionnaire)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
