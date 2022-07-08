// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireNotifyRiskRuleConfiguration : IEntityTypeConfiguration<QuestionnaireNotifyRiskRule>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireNotifyRiskRule> builder)
        {
            builder.ToTable("QuestionnaireNotifyRiskRule");

            builder.Property(ci => ci.QuestionnaireNotifyRiskRuleID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
                   .WithMany()
                   .HasForeignKey(d => d.UpdateUserID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.NotificationLevel)
                   .WithMany()
                   .HasForeignKey(d => d.NotificationLevelID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.QuestionnaireNotifyRiskSchedule)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireNotifyRiskScheduleID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
