// -----------------------------------------------------------------------
// <copyright file="SystemRoleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="SystemRoleConfiguration" />.
    /// </summary>
    public class SystemRoleConfiguration : IEntityTypeConfiguration<SystemRole>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{SystemRole}"/>.</param>
        public void Configure(EntityTypeBuilder<SystemRole> builder)
        {
            builder.ToTable("SystemRole", "info");

            builder.Property(ci => ci.SystemRoleID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
                    .WithMany()
                    .HasForeignKey(d => d.UpdateUserID)
                    .OnDelete(DeleteBehavior.Restrict); ;

        }
    }
}
