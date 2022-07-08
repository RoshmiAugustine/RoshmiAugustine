// -----------------------------------------------------------------------
// <copyright file="AgencyConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.ToTable("Agency");

            builder.Property(ci => ci.AgencyID)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.AgencyIndex)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("newid()");

            builder.HasIndex(b => b.AgencyIndex)
            .IsClustered(false);

            builder.Property(ci => ci.Name)
              .HasMaxLength(150)
              .IsUnicode(false)
              .IsRequired();

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.Property(e => e.Abbrev)
                  .HasMaxLength(50)
                  .IsUnicode(false);

            builder.Property(e => e.Note).IsUnicode(false);

            builder.Property(ci => ci.Email)
                   .HasMaxLength(255)
                   .IsUnicode(false);

            builder.Property(ci => ci.Phone1)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(ci => ci.Phone2)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(ci => ci.ContactFirstName)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(ci => ci.ContactLastName)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.HasOne(s => s.UpdateUser)
             .WithMany()
             .HasForeignKey(d => d.UpdateUserID)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}