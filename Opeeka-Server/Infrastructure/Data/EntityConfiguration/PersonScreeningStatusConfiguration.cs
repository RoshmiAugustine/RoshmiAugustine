// -----------------------------------------------------------------------
// <copyright file="PersonSupportConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;


namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonScreeningStatusConfiguration : IEntityTypeConfiguration<PersonScreeningStatus>
    {
        public void Configure(EntityTypeBuilder<PersonScreeningStatus> builder)
        {
            builder.ToTable("PersonScreeningStatus", "info");

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
