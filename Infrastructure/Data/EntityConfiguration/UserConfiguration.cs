// -----------------------------------------------------------------------
// <copyright file="UserConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(ci => ci.UserID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UserIndex)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID ( )");

            builder.HasIndex(ci => ci.UserIndex)
                .IsClustered(false);

            builder.Property(ci => ci.LastLogin)
                .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.NotificationViewedOn)
                .HasDefaultValueSql("getdate()");
        }
    }
}
