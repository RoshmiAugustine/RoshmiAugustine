// -----------------------------------------------------------------------
// <copyright file="PersonSupportConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonSupportConfiguration : IEntityTypeConfiguration<PersonSupport>
    {
        public void Configure(EntityTypeBuilder<PersonSupport> builder)
        {
            builder.ToTable("PersonSupport");

            builder.Property(ci => ci.PersonSupportID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.FirstName)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(ci => ci.MiddleName)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(ci => ci.LastName)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(ci => ci.Suffix)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(ci => ci.Email)
                .HasMaxLength(255)
                .IsUnicode(false); ;

            builder.Property(ci => ci.PhoneCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(ci => ci.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(ci => ci.Note).IsUnicode(false);

            builder.Property(ci => ci.StartDate)
                .HasDefaultValueSql("getdate()");


            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");


            builder.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonID)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.SupportType)
                .WithMany()
                .HasForeignKey(d => d.SupportTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.SMSConsentStoppedON)
                .HasDefaultValueSql(null);
        }
    }
}
