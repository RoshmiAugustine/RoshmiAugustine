using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ImportTypeConfiguration : IEntityTypeConfiguration<ImportType>
    {
        public void Configure(EntityTypeBuilder<ImportType> builder)
        {
            builder.ToTable("ImportType", "info");

            builder.Property(ci => ci.ImportTypeID).ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).HasMaxLength(100);
            builder.Property(ci => ci.TemplateJson);
            builder.Property(ci => ci.TemplateURL).HasMaxLength(250);
            builder.Property(ci => ci.UpdateDate).HasDefaultValueSql("getdate()");
        }
    }
}
