// -----------------------------------------------------------------------
// <copyright file="CollaborationConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationConfiguration : IEntityTypeConfiguration<Collaboration>
    {
        public void Configure(EntityTypeBuilder<Collaboration> builder)
        {
            builder.ToTable("Collaboration");

            builder.Property(ci => ci.CollaborationID)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.CollaborationIndex)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("newid()");

            builder.HasIndex(b => b.CollaborationIndex)
            .IsClustered(false);

            builder.Property(ci => ci.UpdateDate).IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.Agency)
              .WithMany()
              .HasForeignKey(d => d.AgencyID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.CollaborationLevel)
              .WithMany()
              .HasForeignKey(d => d.CollaborationLevelID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.TherapyType)
             .WithMany()
             .HasForeignKey(d => d.TherapyTypeID)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.UpdateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
