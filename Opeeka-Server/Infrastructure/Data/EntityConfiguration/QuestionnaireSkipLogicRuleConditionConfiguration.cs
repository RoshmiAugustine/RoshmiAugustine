// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleConditionConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class QuestionnaireSkipLogicRuleConditionConfiguration : IEntityTypeConfiguration<QuestionnaireSkipLogicRuleCondition>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireSkipLogicRuleCondition> builder)
        {
            builder.ToTable("QuestionnaireSkipLogicRuleCondition");

            builder.Property(ci => ci.QuestionnaireSkipLogicRuleConditionID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.QuestionnaireItem)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireItemID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.QuestionnaireSkipLogicRule)
                   .WithMany()
                   .HasForeignKey(d => d.QuestionnaireSkipLogicRuleID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                   .WithMany()
                   .HasForeignKey(d => d.UpdateUserID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.ComparisonValue).HasColumnType("decimal(16,2)");
        }
    }
}