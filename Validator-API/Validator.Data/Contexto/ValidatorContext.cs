﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Processo> Processos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnoBaseMap());
            modelBuilder.ApplyConfiguration(new PlanilhaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new DivisaoMap());
            modelBuilder.ApplyConfiguration(new SetorMap());
            modelBuilder.ApplyConfiguration(new ParametroMap());
            modelBuilder.ApplyConfiguration(new UsuarioAvaliadorMap());
            modelBuilder.ApplyConfiguration(new ProcessoMap());
            modelBuilder.ApplyConfiguration(new ProgressoMap());

            modelBuilder.Entity<Parametro>().HasQueryFilter(q => !q.AnoBase.Deleted && q.AnoBase.Ano == DateTime.Now.Year);
            modelBuilder.Entity<Processo>().HasQueryFilter(q => !q.AnoBase.Deleted && q.AnoBase.Ano == DateTime.Now.Year);
            modelBuilder.Entity<Setor>().HasQueryFilter(q => !q.AnoBase.Deleted && q.AnoBase.Ano == DateTime.Now.Year);
            modelBuilder.Entity<Divisao>().HasQueryFilter(q => !q.AnoBase.Deleted && q.AnoBase.Ano == DateTime.Now.Year);
          
            base.OnModelCreating(modelBuilder);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
