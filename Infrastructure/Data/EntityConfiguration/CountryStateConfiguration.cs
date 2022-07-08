// -----------------------------------------------------------------------
// <copyright file="CountryStateConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class CountryStateConfiguration : IEntityTypeConfiguration<CountryState>
    {
        public void Configure(EntityTypeBuilder<CountryState> builder)
        {
            builder.ToTable("CountryState", "info");

            builder.Property(ci => ci.CountryStateID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.Country)
                  .WithMany()
                  .HasForeignKey(d => d.CountryID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                  .WithMany()
                  .HasForeignKey(d => d.UpdateUserID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
