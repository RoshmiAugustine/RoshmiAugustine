// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonQuestionnaireConfiguration : IEntityTypeConfiguration<PersonQuestionnaire>
    {
        public void Configure(EntityTypeBuilder<PersonQuestionnaire> builder)
        {
            builder.ToTable("PersonQuestionnaire");

            builder.Property(ci => ci.PersonQuestionnaireID)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Collaboration)
                .WithMany()
                .HasForeignKey(d => d.CollaborationID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Questionnaire)
                .WithMany()
                .HasForeignKey(d => d.QuestionnaireID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
