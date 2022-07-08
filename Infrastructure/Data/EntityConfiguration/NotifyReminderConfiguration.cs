// -----------------------------------------------------------------------
// <copyright file="NotifyReminderConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="NotifyReminderConfiguration" />.
    /// </summary>
    public class NotifyReminderConfiguration : IEntityTypeConfiguration<NotifyReminder>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{NotifyReminder}"/>.</param>
        public void Configure(EntityTypeBuilder<NotifyReminder> builder)
        {
            builder.ToTable("NotifyReminder");

            builder.Property(ci => ci.NotifyReminderID)
                .ValueGeneratedOnAdd();
            builder.Property(ci => ci.InviteToCompleteMailStatus).HasMaxLength(10);

            builder.HasOne(ci => ci.QuestionnaireReminderRule)
                .WithMany()
                .HasForeignKey(f => f.QuestionnaireReminderRuleID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
