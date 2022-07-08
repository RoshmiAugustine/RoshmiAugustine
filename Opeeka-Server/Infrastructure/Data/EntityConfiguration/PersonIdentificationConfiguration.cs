// -----------------------------------------------------------------------
// <copyright file="PersonIdentificationConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonIdentificationConfiguration : IEntityTypeConfiguration<PersonIdentification>
    {
        public void Configure(EntityTypeBuilder<PersonIdentification> builder)
        {
            builder.ToTable("PersonIdentification");

            builder.Property(ci => ci.PersonIdentificationID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.IdentificationNumber)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.IdentificationType)
                .WithMany()
                .HasForeignKey(d => d.IdentificationTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
