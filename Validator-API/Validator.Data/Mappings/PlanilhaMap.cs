﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class PlanilhaMap : IEntityTypeConfiguration<Planilha>
    {
        public void Configure(EntityTypeBuilder<Planilha> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Unidade).HasMaxLength(30);
            builder.Property(c => c.CPF).HasMaxLength(20);
            builder.Property(c => c.Nome).HasMaxLength(180);
            builder.Property(c => c.Email).HasMaxLength(120);
            builder.Property(c => c.Cargo).HasMaxLength(60);
            builder.Property(c => c.Nivel).HasMaxLength(60);
            builder.Property(c => c.CentroCusto).HasMaxLength(60);
            builder.Property(c => c.NumeroCentroCusto).HasMaxLength(60);
            builder.Property(c => c.SuperiorImediato).HasMaxLength(180);
            builder.Property(c => c.EmailSuperior).HasMaxLength(180);
            builder.Property(c => c.Direcao).HasMaxLength(120);
            builder.Property(c => c.GestorCorporativo).HasMaxLength(120);
            builder.Property(c => c.Validacoes);

            builder.HasOne(c => c.AnoBase)
                .WithMany()
                .HasForeignKey(c => c.AnoBaseId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
