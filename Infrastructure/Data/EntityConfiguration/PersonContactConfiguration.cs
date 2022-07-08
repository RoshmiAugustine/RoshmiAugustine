// -----------------------------------------------------------------------
// <copyright file="PersonContactConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonContactConfiguration : IEntityTypeConfiguration<PersonContact>
    {
        public void Configure(EntityTypeBuilder<PersonContact> builder)
        {
            builder.ToTable("PersonContact");

            builder.HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Contact)
                .WithMany()
                .HasForeignKey(d => d.ContactID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
