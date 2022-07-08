using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class AuditPersonProfileConfiguration : IEntityTypeConfiguration<AuditPersonProfile>
    {
        public void Configure(EntityTypeBuilder<AuditPersonProfile> builder)
        {
            builder.ToTable("AuditPersonProfile");

            builder.Property(ci => ci.AuditPersonProfileID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.TypeName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(ci => ci.UpdateDate)
             .HasDefaultValueSql("getdate()");

            builder.HasOne(d => d.UpdateUser)
                 .WithMany()
                 .HasForeignKey(d => d.UpdateUserID)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
