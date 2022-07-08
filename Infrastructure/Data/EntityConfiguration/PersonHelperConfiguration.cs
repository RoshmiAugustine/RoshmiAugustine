// -----------------------------------------------------------------------
// <copyright file="PersonHelperConfiguration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class PersonHelperConfiguration : IEntityTypeConfiguration<PersonHelper>
    {
        public void Configure(EntityTypeBuilder<PersonHelper> builder)
        {
            builder.ToTable("PersonHelper");

            builder.Property(ci => ci.PersonHelperID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.Helper)
                .WithMany()
                .HasForeignKey(d => d.HelperID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.CollaborationID)
                .HasDefaultValue(null);


        }
    }
}
