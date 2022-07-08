// -----------------------------------------------------------------------
// <copyright file="NotificationResolutionStatusConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="NotificationResolutionStatusConfiguration" />.
    /// </summary>
    public class NotificationResolutionStatusConfiguration : IEntityTypeConfiguration<NotificationResolutionStatus>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{NotificationResolutionStatus}"/>.</param>
        public void Configure(EntityTypeBuilder<NotificationResolutionStatus> builder)
        {
            builder.ToTable("NotificationResolutionStatus", "info");

            builder.Property(ci => ci.NotificationResolutionStatusID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(ci => ci.UpdateUser)
                .WithMany()
                .HasForeignKey(f => f.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
