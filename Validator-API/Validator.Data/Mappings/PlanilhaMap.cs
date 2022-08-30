using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class PlanilhaMap : IEntityTypeConfiguration<Planilha>
    {
        public void Configure(EntityTypeBuilder<Planilha> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Unidade).HasMaxLength(15);
            builder.Property(c => c.CPF).HasMaxLength(15);
            builder.Property(c => c.Nome).HasMaxLength(120);
            builder.Property(c => c.Email).HasMaxLength(120);
            builder.Property(c => c.Cargo).HasMaxLength(60);
            builder.Property(c => c.Nivel).HasMaxLength(60);
            builder.Property(c => c.CentroCusto).HasMaxLength(60);
            builder.Property(c => c.NumeroCentroCusto).HasMaxLength(15);
            builder.Property(c => c.SuperiorImediato).HasMaxLength(120);
            builder.Property(c => c.EmailSuperior).HasMaxLength(120);
            builder.Property(c => c.Direcao).HasMaxLength(120);
            builder.Property(c => c.Validacoes).HasMaxLength(180);

            builder.HasOne(c => c.AnoBase)
                .WithMany()
                .HasForeignKey(c => c.AnoBaseId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
