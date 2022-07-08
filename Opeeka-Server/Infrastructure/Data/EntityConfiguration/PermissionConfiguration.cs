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
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", "info");

            builder.Property(ci => ci.PermissionID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.UpdateUser)
               .WithMany()
               .HasForeignKey(d => d.UpdateUserID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.ApplicationObject)
               .WithMany()
               .HasForeignKey(d => d.ApplicationObjectID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.OperationType)
               .WithMany()
               .HasForeignKey(d => d.OperationTypeID)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
