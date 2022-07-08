// -----------------------------------------------------------------------
// <copyright file="CollaborationLevelConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationLevelConfiguration : IEntityTypeConfiguration<CollaborationLevel>
    {
        public void Configure(EntityTypeBuilder<CollaborationLevel> builder)
        {
            builder.ToTable("CollaborationLevel", "info");

            builder.Property(ci => ci.CollaborationLevelID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate).IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.Agency)
                .WithMany()
                .HasForeignKey(d => d.AgencyID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.UpdateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
