using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Infrastructure.Data.EntityConfiguration
{
    public class ExportTemplateAgencyConfiguration : IEntityTypeConfiguration<ExportTemplateAgency>
    {
        public void Configure(EntityTypeBuilder<ExportTemplateAgency> builder)
        {
            builder.ToTable("ExportTemplateAgency");

            builder.Property(ci => ci.ExportTemplateAgencyID)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Agency)
             .WithMany()
             .HasForeignKey(d => d.AgencyID)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.ExportTemplate)
             .WithMany()
             .HasForeignKey(d => d.ExportTemplateID)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
