// -----------------------------------------------------------------------
// <copyright file="ContactConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact", "info");

            builder.Property(ci => ci.ContactID)
                .ValueGeneratedOnAdd();

            builder.HasOne(d => d.ContactType)
                 .WithMany()
                 .HasForeignKey(d => d.ContactTypeID)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
