// -----------------------------------------------------------------------
// <copyright file="UserRoleConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ApplicationObjectConfiguration : IEntityTypeConfiguration<ApplicationObject>
    {
        public void Configure(EntityTypeBuilder<ApplicationObject> builder)
        {
            builder.ToTable("ApplicationObject");

            builder.Property(ci => ci.ApplicationObjectID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.UpdateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.ApplicationObjectType)
              .WithMany()
              .HasForeignKey(d => d.ApplicationObjectTypeID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
