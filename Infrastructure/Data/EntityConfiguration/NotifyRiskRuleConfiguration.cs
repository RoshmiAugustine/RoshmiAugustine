// -----------------------------------------------------------------------
// <copyright file="NotifyRiskRuleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class NotifyRiskRuleConfiguration : IEntityTypeConfiguration<NotifyRiskRule>
    {
        public void Configure(EntityTypeBuilder<NotifyRiskRule> builder)
        {
            builder.ToTable("NotifyRiskRule");

            builder.Property(ci => ci.NotifyRiskRuleID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(ci => ci.UpdateUser)
                .WithMany()
                .HasForeignKey(f => f.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.NotificationLevel)
                .WithMany()
                .HasForeignKey(f => f.NotificationLevelID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.QuestionnaireItem)
                .WithMany()
                .HasForeignKey(f => f.QuestionnaireItemID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
