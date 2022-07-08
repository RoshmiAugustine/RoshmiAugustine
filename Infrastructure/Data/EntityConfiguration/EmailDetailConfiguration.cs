using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class EmailDetailConfiguration : IEntityTypeConfiguration<EmailDetail>
    {
        public void Configure(EntityTypeBuilder<EmailDetail> builder)
        {
            builder.ToTable("EmailDetail");

            builder.Property(ci => ci.EmailDetailID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.Status)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(ci => ci.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(ci => ci.UpdateDate)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Helper)
                .WithMany()
                .HasForeignKey(d => d.HelperID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Agency)
                .WithMany()
                .HasForeignKey(d => d.AgencyID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
