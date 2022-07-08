// -----------------------------------------------------------------------
// <copyright file="NotifyRiskConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="NotifyRiskConfiguration" />.
    /// </summary>
    public class NotifyRiskConfiguration : IEntityTypeConfiguration<NotifyRisk>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{NotifyRisk}"/>.</param>
        public void Configure(EntityTypeBuilder<NotifyRisk> builder)
        {
            builder.ToTable("NotifyRisk");

            builder.Property(ci => ci.NotifyRiskID)
               .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(ci => ci.UpdateUser)
                .WithMany()
                .HasForeignKey(f => f.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Assessment)
              .WithMany()
              .HasForeignKey(f => f.AssessmentID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.QuestionnaireNotifyRiskRule)
              .WithMany()
              .HasForeignKey(f => f.QuestionnaireNotifyRiskRuleID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Person)
                .WithMany()
                .HasForeignKey(f => f.PersonID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
