// -----------------------------------------------------------------------
// <copyright file="NotificationModeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class NotificationModeConfiguration : IEntityTypeConfiguration<NotificationMode>
    {
        public void Configure(EntityTypeBuilder<NotificationMode> builder)
        {
            builder.ToTable("NotificationMode", "info");

            builder.Property(ci => ci.NotificationModeID)
                .ValueGeneratedOnAdd();
        }
    }
}
