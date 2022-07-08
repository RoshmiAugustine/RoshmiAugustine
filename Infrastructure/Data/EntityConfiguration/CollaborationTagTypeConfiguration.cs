// -----------------------------------------------------------------------
// <copyright file="CollaborationTagTypeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CollaborationTagTypeConfiguration : IEntityTypeConfiguration<CollaborationTagType>
    {
        public void Configure(EntityTypeBuilder<CollaborationTagType> builder)
        {
            builder.ToTable("CollaborationTagType", "info");

            builder.Property(ci => ci.CollaborationTagTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
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
