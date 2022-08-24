using Microsoft.EntityFrameworkCore;
using Validator.Data.Mappings;
using Validator.Domain.Entities;

namespace Validator.Data.Contexto
{
    public class ValidatorContext : DbContext
    {
        public ValidatorContext(DbContextOptions<ValidatorContext> options) : base(options)
        {

        }

        public DbSet<AnoBase> AnoBases { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Planilha> Planilhas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnoBaseMap());
            modelBuilder.ApplyConfiguration(new PlanilhaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
