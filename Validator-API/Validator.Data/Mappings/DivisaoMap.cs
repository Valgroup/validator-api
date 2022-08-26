using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class DivisaoMap : IEntityTypeConfiguration<Divisao>
    {
        public void Configure(EntityTypeBuilder<Divisao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).HasMaxLength(20);

            builder.HasOne(c => c.AnoBase)
                .WithMany()
                .HasForeignKey(c => c.AnoBaseId);
        }
    }
}
