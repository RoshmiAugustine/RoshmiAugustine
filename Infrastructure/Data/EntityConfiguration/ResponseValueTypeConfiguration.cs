// -----------------------------------------------------------------------
// <copyright file="ResponseValueTypeConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    class ResponseValueTypeConfiguration : IEntityTypeConfiguration<ResponseValueType>
    {
        public void Configure(EntityTypeBuilder<ResponseValueType> builder)
        {
            builder.ToTable("ResponseValueType", "info");

            builder.Property(ci => ci.ResponseValueTypeID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
               .WithMany()
               .HasForeignKey(d => d.UpdateUserID)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
