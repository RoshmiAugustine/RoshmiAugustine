// -----------------------------------------------------------------------
// <copyright file="PersonCollaborationConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class PersonCollaborationConfiguration : IEntityTypeConfiguration<PersonCollaboration>
    {
        public void Configure(EntityTypeBuilder<PersonCollaboration> builder)
        {
            builder.ToTable("PersonCollaboration");

            builder.Property(ci => ci.PersonCollaborationID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.Collaboration)
                .WithMany()
                .HasForeignKey(d => d.CollaborationID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
