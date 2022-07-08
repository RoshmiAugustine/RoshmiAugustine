// -----------------------------------------------------------------------
// <copyright file="PersonConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.Property(ci => ci.PersonID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.PersonIndex)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID ( )");

            builder.HasIndex(ci => ci.PersonIndex)
                .IsClustered(false);

            builder.Property(ci => ci.FirstName)
                   .HasMaxLength(150)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(ci => ci.MiddleName)
                   .HasMaxLength(150)
                   .IsUnicode(false);

            builder.Property(ci => ci.LastName)
                   .HasMaxLength(150)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(ci => ci.Suffix)
                   .HasMaxLength(50)
                   .IsUnicode(false);


            builder.Property(ci => ci.Email)
                   .HasMaxLength(255)
                   .IsUnicode(false);

            builder.Property(ci => ci.Phone1Code)
                   .HasMaxLength(10)
                   .IsUnicode(false);

            builder.Property(ci => ci.Phone2Code)
                   .HasMaxLength(10)
                   .IsUnicode(false);

            builder.Property(ci => ci.Phone1)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(ci => ci.Phone2)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.Property(ci => ci.GenderID);

            builder.HasOne(s => s.Sexuality)
                .WithMany()
                .HasForeignKey(d => d.SexualityID);

            builder.HasOne(s => s.BiologicalSex)
                .WithMany()
                .HasForeignKey(d => d.BiologicalSexID);

            builder.HasOne(s => s.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Agency)
                .WithMany()
                .HasForeignKey(d => d.AgencyID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.PrimaryLanguage)
                .WithMany()
                .HasForeignKey(d => d.PrimaryLanguageID);

            builder.HasOne(d => d.PreferredLanguage)
                .WithMany()
                .HasForeignKey(d => d.PreferredLanguageID);

            builder.HasOne(d => d.PersonScreeningStatus)
                .WithMany()
                .HasForeignKey(d => d.PersonScreeningStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.SMSConsentStoppedON)
                .HasDefaultValueSql(null);

        }
    }
}
