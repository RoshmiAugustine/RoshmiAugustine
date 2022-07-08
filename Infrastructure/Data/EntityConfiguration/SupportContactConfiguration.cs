// -----------------------------------------------------------------------
// <copyright file="SupportContact.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class SupportContactConfiguration : IEntityTypeConfiguration<SupportContact>
    {
        public void Configure(EntityTypeBuilder<SupportContact> builder)
        {
            builder.ToTable("SupportContact");
            builder.Property(ci => ci.SupportContactID)
                 .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Contact)
                   .WithMany()
                   .HasForeignKey(d => d.ContactID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Support)
                   .WithMany()
                   .HasForeignKey(d => d.SupportID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
