using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    internal class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome).HasMaxLength(120);
            builder.Property(c => c.Email).HasMaxLength(120);
            builder.Property(c => c.EmailSuperior).HasMaxLength(120);
            builder.Property(c => c.Cargo).HasMaxLength(30);
            builder.Property(c => c.Senha).HasMaxLength(180);

            builder.HasOne(c => c.AnoBase)
                .WithMany()
                .HasForeignKey(c => c.AnoBaseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Setor)
                .WithMany()
                .HasForeignKey(c => c.SetorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Divisao)
               .WithMany()
               .HasForeignKey(c => c.DivisaoId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Superior)
                .WithMany()
                .HasForeignKey(c => c.SuperiorId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
