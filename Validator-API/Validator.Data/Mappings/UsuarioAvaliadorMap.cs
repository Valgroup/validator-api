using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class UsuarioAvaliadorMap : IEntityTypeConfiguration<UsuarioAvaliador>
    {
        public void Configure(EntityTypeBuilder<UsuarioAvaliador> builder)
        {
            builder.HasKey(c => new { c.UsuarioId, c.AvaliadorId });

            builder.HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Avaliador)
                .WithMany()
                .HasForeignKey(c => c.AvaliadorId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
