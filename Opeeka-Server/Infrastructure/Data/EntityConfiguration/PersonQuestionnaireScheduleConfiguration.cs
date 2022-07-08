// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireScheduleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class PersonQuestionnaireScheduleConfiguration : IEntityTypeConfiguration<PersonQuestionnaireSchedule>
    {
        public void Configure(EntityTypeBuilder<PersonQuestionnaireSchedule> builder)
        {
            builder.ToTable("PersonQuestionnaireSchedule");

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.PersonQuestionnaire)
                .WithMany()
                .HasForeignKey(d => d.PersonQuestionnaireID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.QuestionnaireWindow)
                .WithMany()
                .HasForeignKey(d => d.QuestionnaireWindowID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
