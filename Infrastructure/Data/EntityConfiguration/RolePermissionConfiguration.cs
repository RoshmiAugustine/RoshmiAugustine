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
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermission", "info");

            builder.Property(ci => ci.RolePermissionID)
                .ValueGeneratedOnAdd();

            builder.HasOne(d => d.UserRole)
                  .WithMany()
                  .HasForeignKey(d => d.UserRoleID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Permission)
                  .WithMany()
                  .HasForeignKey(d => d.PermissionID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
