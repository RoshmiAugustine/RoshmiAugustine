// -----------------------------------------------------------------------
// <copyright file="PersonLanguageConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonLanguageConfiguration : IEntityTypeConfiguration<PersonLanguage>
    {
        public void Configure(EntityTypeBuilder<PersonLanguage> builder)
        {
            builder.ToTable("PersonLanguage");

            builder.HasKey(e => new { e.PersonID, e.LanguageID });

            builder.HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Language)
                .WithMany()
                .HasForeignKey(d => d.LanguageID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
