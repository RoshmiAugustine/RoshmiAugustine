// -----------------------------------------------------------------------
// <copyright file="CollaborationQuestionnaireConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationQuestionnaireConfiguration : IEntityTypeConfiguration<CollaborationQuestionnaire>
    {
        public void Configure(EntityTypeBuilder<CollaborationQuestionnaire> builder)
        {
            builder.ToTable("CollaborationQuestionnaire");

            builder.Property(ci => ci.CollaborationQuestionnaireID)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Collaboration)
                .WithMany()
                .HasForeignKey(d => d.CollaborationID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Questionnaire)
              .WithMany()
              .HasForeignKey(d => d.QuestionnaireID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}