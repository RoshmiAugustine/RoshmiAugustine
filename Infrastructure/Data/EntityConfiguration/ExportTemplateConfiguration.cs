using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ExportTemplateConfiguration : IEntityTypeConfiguration<ExportTemplate>
    {
        public void Configure(EntityTypeBuilder<ExportTemplate> builder)
        {
            builder.ToTable("ExportTemplate");

            builder.Property(ci => ci.ExportTemplateID)
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.TemplateType)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
        }
    }
}
