// -----------------------------------------------------------------------
// <copyright file="NotificationLogConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="NotificationLogConfiguration" />.
    /// </summary>
    public class NotificationLogConfiguration : IEntityTypeConfiguration<NotificationLog>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{NotificationLog}"/>.</param>
        public void Configure(EntityTypeBuilder<NotificationLog> builder)
        {
            builder.ToTable("NotificationLog");

            builder.Property(ci => ci.NotificationLogID)
               .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
              .HasDefaultValueSql("getdate()");

            builder.HasOne(ci => ci.Person)
              .WithMany()
              .HasForeignKey(f => f.PersonID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.NotificationResolutionStatus)
               .WithMany()
               .HasForeignKey(f => f.NotificationResolutionStatusID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.NotificationType)
               .WithMany()
               .HasForeignKey(f => f.NotificationTypeID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.HelperName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .IsRequired(false);
        }
    }
}
