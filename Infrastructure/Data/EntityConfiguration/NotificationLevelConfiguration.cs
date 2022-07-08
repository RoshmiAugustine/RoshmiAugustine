// -----------------------------------------------------------------------
// <copyright file="NotificationLevelConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class NotificationLevelConfiguration : IEntityTypeConfiguration<NotificationLevel>
    {
        public void Configure(EntityTypeBuilder<NotificationLevel> builder)
        {
            builder.ToTable("NotificationLevel", "info");

            builder.Property(ci => ci.NotificationLevelID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(ci => ci.UpdateUser)
                .WithMany()
                .HasForeignKey(f => f.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Agency)
                .WithMany()
                .HasForeignKey(f => f.AgencyID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.NotificationType)
                .WithMany()
                .HasForeignKey(f => f.NotificationTypeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
