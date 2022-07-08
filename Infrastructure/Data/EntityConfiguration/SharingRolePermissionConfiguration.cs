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
    public class SharingRolePermissionConfiguration : IEntityTypeConfiguration<SharingRolePermission>
    {
        public void Configure(EntityTypeBuilder<SharingRolePermission> builder)
        {
            builder.ToTable("SharingRolePermission", "info");

            builder.HasKey(ci => ci.SharingRolePermissionID);

            builder.Property(ci => ci.SharingRolePermissionID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.SystemRolePermissionID)
                .IsRequired();

            builder.Property(ci => ci.AgencySharingPolicyID)
                .IsRequired();
            builder.Property(ci => ci.CollaborationSharingPolicyID)
                .IsRequired();
        }
    }
}
