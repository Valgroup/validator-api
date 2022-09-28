﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Validator.Data.Contexto;

#nullable disable

namespace Validator.Data.Migrations
{
    [DbContext(typeof(ValidatorContext))]
    [Migration("20220928112013_V_1_0_16")]
    partial class V_1_0_16
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Validator.Domain.Entities.AnoBase", b =>
                {
                    b.Property<Guid>("AnoBaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.HasKey("AnoBaseId");

                    b.ToTable("AnoBases");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Divisao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnoBaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AnoBaseId");

                    b.ToTable("Divisao");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Parametro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnoBaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DhFinalizacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("QtdDiaFinaliza")
                        .HasColumnType("int");

                    b.Property<int>("QtdeAvaliador")
                        .HasColumnType("int");

                    b.Property<int>("QtdeSugestaoMax")
                        .HasColumnType("int");

                    b.Property<int>("QtdeSugestaoMin")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnoBaseId");

                    b.ToTable("Parametro");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Planilha", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnoBaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Cargo")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("CentroCusto")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime?>("DataAdmissao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Direcao")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<bool>("EhValido")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("EmailSuperior")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Nivel")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Nome")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("NumeroCentroCusto")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("SuperiorImediato")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Unidade")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Validacoes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnoBaseId");

                    b.ToTable("Planilhas");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Processo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnoBaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DhFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DhInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnoBaseId");

                    b.ToTable("Processos");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Progresso", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnoBaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnoBaseId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Progresso");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Setor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnoBaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("AnoBaseId");

                    b.ToTable("Setor");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnoBaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid>("AzureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cargo")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DivisaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Documento")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("EhDiretor")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("EmailSuperior")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<Guid?>("SetorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SuperiorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnoBaseId");

                    b.HasIndex("DivisaoId");

                    b.HasIndex("SetorId");

                    b.HasIndex("SuperiorId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Validator.Domain.Entities.UsuarioAvaliador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AvaliadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AvaliadorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioAvaliador");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Divisao", b =>
                {
                    b.HasOne("Validator.Domain.Entities.AnoBase", "AnoBase")
                        .WithMany()
                        .HasForeignKey("AnoBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnoBase");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Parametro", b =>
                {
                    b.HasOne("Validator.Domain.Entities.AnoBase", "AnoBase")
                        .WithMany()
                        .HasForeignKey("AnoBaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AnoBase");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Planilha", b =>
                {
                    b.HasOne("Validator.Domain.Entities.AnoBase", "AnoBase")
                        .WithMany()
                        .HasForeignKey("AnoBaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AnoBase");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Processo", b =>
                {
                    b.HasOne("Validator.Domain.Entities.AnoBase", "AnoBase")
                        .WithMany()
                        .HasForeignKey("AnoBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnoBase");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Progresso", b =>
                {
                    b.HasOne("Validator.Domain.Entities.AnoBase", "AnoBase")
                        .WithMany()
                        .HasForeignKey("AnoBaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Validator.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AnoBase");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Setor", b =>
                {
                    b.HasOne("Validator.Domain.Entities.AnoBase", "AnoBase")
                        .WithMany()
                        .HasForeignKey("AnoBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnoBase");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("Validator.Domain.Entities.AnoBase", "AnoBase")
                        .WithMany()
                        .HasForeignKey("AnoBaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Validator.Domain.Entities.Divisao", "Divisao")
                        .WithMany()
                        .HasForeignKey("DivisaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Validator.Domain.Entities.Setor", "Setor")
                        .WithMany()
                        .HasForeignKey("SetorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Validator.Domain.Entities.Usuario", "Superior")
                        .WithMany()
                        .HasForeignKey("SuperiorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AnoBase");

                    b.Navigation("Divisao");

                    b.Navigation("Setor");

                    b.Navigation("Superior");
                });

            modelBuilder.Entity("Validator.Domain.Entities.UsuarioAvaliador", b =>
                {
                    b.HasOne("Validator.Domain.Entities.Usuario", "Avaliador")
                        .WithMany()
                        .HasForeignKey("AvaliadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Validator.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Avaliadores")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Avaliador");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Validator.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("Avaliadores");
                });
#pragma warning restore 612, 618
        }
    }
}
