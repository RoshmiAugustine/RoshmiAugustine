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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");

            builder.Property(ci => ci.UserRoleID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UserRoleIndex)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID ( )");

            builder.HasIndex(ci => ci.UserRoleIndex)
                .IsClustered(false);

            builder.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.SystemRole)
                    .WithMany()
                    .HasForeignKey(d => d.SystemRoleID)
                    .OnDelete(DeleteBehavior.Restrict); ;

        }
    }
}
