// -----------------------------------------------------------------------
// <copyright file="QuestionnaireReminderRuleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireReminderRuleConfiguration : IEntityTypeConfiguration<QuestionnaireReminderRule>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireReminderRule> builder)
        {
            builder.ToTable("QuestionnaireReminderRule");

            builder.Property(ci => ci.ReminderOffsetTypeID)
                .HasColumnType("char(1)")
                .IsRequired(false)
                .HasMaxLength(1).HasDefaultValue('d');

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.QuestionnaireReminderRuleID)
                .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Questionnaire)
                  .WithMany()
                  .HasForeignKey(d => d.QuestionnaireID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.QuestionnaireReminderType)
                  .WithMany()
                  .HasForeignKey(d => d.QuestionnaireReminderTypeID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
