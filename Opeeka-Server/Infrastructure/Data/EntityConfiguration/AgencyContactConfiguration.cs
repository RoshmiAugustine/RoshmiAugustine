// -----------------------------------------------------------------------
// <copyright file="AgencyContactConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AgencyContactConfiguration : IEntityTypeConfiguration<AgencyContact>
    {
        public void Configure(EntityTypeBuilder<AgencyContact> builder)
        {
            builder.Property(e => e.AgencyContactID)
                    .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Agency)
                 .WithMany()
                 .HasForeignKey(d => d.AgencyID)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Contact)
                .WithMany()
                .HasForeignKey(d => d.ContactID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
