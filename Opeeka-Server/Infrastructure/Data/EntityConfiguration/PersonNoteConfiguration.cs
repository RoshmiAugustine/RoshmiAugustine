// -----------------------------------------------------------------------
// <copyright file="PersonNoteConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonNoteConfiguration : IEntityTypeConfiguration<PersonNote>
    {
        public void Configure(EntityTypeBuilder<PersonNote> builder)
        {
            builder.ToTable("PersonNote");

            builder.Property(ci => ci.PersonNoteID)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Note)
                .WithMany()
                .HasForeignKey(d => d.NoteID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
