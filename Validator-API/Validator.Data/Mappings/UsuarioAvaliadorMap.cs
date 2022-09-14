using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class UsuarioAvaliadorMap : IEntityTypeConfiguration<UsuarioAvaliador>
    {
        public void Configure(EntityTypeBuilder<UsuarioAvaliador> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Usuario)
                .WithMany(m => m.Avaliadores)
                .HasForeignKey(c => c.UsuarioId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Avaliador)
                .WithMany()
                .HasForeignKey(c => c.AvaliadorId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
