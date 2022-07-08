// -----------------------------------------------------------------------
// <copyright file="AddressConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    /// <summary>
    /// Defines the <see cref="AddressConfiguration" />.
    /// </summary>
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="builder">The builder<see cref="EntityTypeBuilder{Address}"/>.</param>
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.Property(ci => ci.AddressID)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.AddressIndex)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("newid()");

            builder.HasIndex(b => b.AddressIndex)
                .IsClustered(false);

            builder.Property(ci => ci.UpdateUserID)
                .IsRequired();

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.UpdateUser)
               .WithMany()
               .HasForeignKey(d => d.UpdateUserID)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
