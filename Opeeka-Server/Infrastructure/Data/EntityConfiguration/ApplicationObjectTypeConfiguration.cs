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
    public class ApplicationObjectTypeConfiguration : IEntityTypeConfiguration<ApplicationObjectType>
    {
        public void Configure(EntityTypeBuilder<ApplicationObjectType> builder)
        {
            builder.ToTable("ApplicationObjectType");

            builder.Property(ci => ci.ApplicationObjectTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.ApplicationObjectTypeID)
                .IsRequired();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.UpdateUser)
              .WithMany()
              .HasForeignKey(d => d.UpdateUserID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
