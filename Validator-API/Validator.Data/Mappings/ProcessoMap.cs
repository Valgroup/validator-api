using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class ProcessoMap : IEntityTypeConfiguration<Processo>
    {
        public void Configure(EntityTypeBuilder<Processo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.AnoBase)
                .WithMany()
                .HasForeignKey(c => c.AnoBaseId);
        }
    }
}
