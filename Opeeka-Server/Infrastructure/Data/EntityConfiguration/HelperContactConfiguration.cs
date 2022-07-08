// -----------------------------------------------------------------------
// <copyright file="HelperContactConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class HelperContactConfiguration : IEntityTypeConfiguration<HelperContact>
    {
        public void Configure(EntityTypeBuilder<HelperContact> builder)
        {
            builder.ToTable("HelperContact");

            builder.Property(ci => ci.HelperContactID)
                 .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Contact)
                .WithMany()
                .HasForeignKey(d => d.ContactID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Helper)
                .WithMany()
                .HasForeignKey(d => d.HelperID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
