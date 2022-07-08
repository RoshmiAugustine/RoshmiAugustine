// -----------------------------------------------------------------------
// <copyright file="ContactTypeConfigurataion.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ContactTypeConfigurataion : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {
            builder.ToTable("ContactType", "info");

            builder.Property(ci => ci.ContactTypeID)
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
