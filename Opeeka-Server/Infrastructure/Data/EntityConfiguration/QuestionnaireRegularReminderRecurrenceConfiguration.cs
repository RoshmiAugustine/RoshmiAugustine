using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{

    public class QuestionnaireRegularReminderRecurrenceConfiguration : IEntityTypeConfiguration<QuestionnaireRegularReminderRecurrence>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireRegularReminderRecurrence> builder)
        {
            builder.ToTable("QuestionnaireRegularReminderRecurrence");

            builder.Property(ci => ci.QuestionnaireRegularReminderRecurrenceID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.RecurrenceDayNameIDs).HasDefaultValue(null).IsRequired().HasMaxLength(30);
            builder.Property(ci => ci.RecurrenceMonthIDs).HasDefaultValue(null).IsRequired().HasMaxLength(30);
            builder.Property(ci => ci.RecurrenceOrdinalIDs).HasDefaultValue(null).IsRequired().HasMaxLength(30);
            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }

    public class QuestionnaireRegularReminderTimeRuleConfiguration : IEntityTypeConfiguration<QuestionnaireRegularReminderTimeRule>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireRegularReminderTimeRule> builder)
        {
            builder.ToTable("QuestionnaireRegularReminderTimeRule");

            builder.Property(ci => ci.QuestionnaireRegularReminderTimeRuleID)
                .ValueGeneratedOnAdd();
            builder.Property(ci => ci.Hour).HasMaxLength(6);
            builder.Property(ci => ci.Minute).HasMaxLength(6);
            builder.Property(ci => ci.AMorPM).HasMaxLength(6);
            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");
        }
    }
}
