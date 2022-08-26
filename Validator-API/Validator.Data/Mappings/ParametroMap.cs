using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class ParametroMap : IEntityTypeConfiguration<Parametro>
    {
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.AnoBase)
                .WithMany()
                .HasForeignKey(c => c.AnoBaseId);
        }
    }
}
