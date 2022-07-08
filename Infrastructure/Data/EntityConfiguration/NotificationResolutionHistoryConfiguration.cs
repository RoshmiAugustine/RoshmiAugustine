// -----------------------------------------------------------------------
// <copyright file="NotificationResolutionHistoryConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="NotificationResolutionHistoryConfiguration" />.
    /// </summary>
    public class NotificationResolutionHistoryConfiguration : IEntityTypeConfiguration<NotificationResolutionHistory>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{NotificationResolutionHistory}"/>.</param>
        public void Configure(EntityTypeBuilder<NotificationResolutionHistory> builder)
        {
            builder.ToTable("NotificationResolutionHistory");

            builder.Property(ci => ci.NotificationResolutionHistoryID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
              .HasDefaultValueSql("getdate()");

            builder.HasOne(ci => ci.StatusFromNavigation)
                .WithMany()
                .HasForeignKey(f => f.StatusFrom)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.StatusToNavigation)
                .WithMany()
                .HasForeignKey(f => f.StatusTo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.NotificationLog)
                .WithMany()
                .HasForeignKey(f => f.NotificationLogID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
