using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class ProgressoMap : IEntityTypeConfiguration<Progresso>
    {
        public void Configure(EntityTypeBuilder<Progresso> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.AnoBase)
                .WithMany()
                .HasForeignKey(c => c.AnoBaseId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
