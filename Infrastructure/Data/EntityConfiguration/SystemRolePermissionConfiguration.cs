// -----------------------------------------------------------------------
// <copyright file="UserRoleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class SystemRolePermissionConfiguration : IEntityTypeConfiguration<SystemRolePermission>
    {
        public void Configure(EntityTypeBuilder<SystemRolePermission> builder)
        {
            builder.ToTable("SystemRolePermission", "info");

            builder.HasKey(ci => ci.SystemRolePermissionID);

            builder.Property(ci => ci.SystemRolePermissionID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.SystemRoleID)
                .IsRequired();

            builder.Property(ci => ci.PermissionID)
                .IsRequired();
        }
    }
}
