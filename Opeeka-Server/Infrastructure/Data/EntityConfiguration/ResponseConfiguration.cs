// -----------------------------------------------------------------------
// <copyright file="ResponseConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ResponseConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.ToTable("Response");

            builder.Property(ci => ci.ResponseID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.ResponseIndex)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql("NEWID ( )");

            builder.HasIndex(ci => ci.ResponseIndex)
                .IsClustered(false);

            builder.Property(ci => ci.IsItemResponseBehaviorDisabled)
                .IsRequired().HasDefaultValue(false);

            builder.Property(ci => ci.UpdateDate)
               .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
                  .WithMany()
                  .HasForeignKey(d => d.UpdateUserID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.Value).HasColumnType("decimal(16,2)");
        }
    }
}
