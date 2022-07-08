// -----------------------------------------------------------------------
// <copyright file="NotifyRiskValueConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class NotifyRiskValueConfiguration : IEntityTypeConfiguration<NotifyRiskValue>
    {
        public void Configure(EntityTypeBuilder<NotifyRiskValue> builder)
        {
            builder.ToTable("NotifyRiskValue");

            builder.Property(ci => ci.NotifyRiskValueID)
                .ValueGeneratedOnAdd();

            builder.HasOne(ci => ci.NotifyRisk)
                .WithMany()
                .HasForeignKey(f => f.NotifyRiskID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.QuestionnaireNotifyRiskRuleCondition)
               .WithMany()
               .HasForeignKey(f => f.QuestionnaireNotifyRiskRuleConditionID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.AssessmentResponse)
               .WithMany()
               .HasForeignKey(f => f.AssessmentResponseID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.AssessmentResponseValue).HasColumnType("decimal(16,2)");
        }
    }
}
