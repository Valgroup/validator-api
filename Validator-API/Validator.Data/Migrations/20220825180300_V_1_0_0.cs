using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Validator.Data.Migrations
{
    public partial class V_1_0_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnoBases",
                columns: table => new
                {
                    AnoBaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    NewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnoBases", x => x.AnoBaseId);
                });

            migrationBuilder.CreateTable(
                name: "Planilhas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unidade = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CentroCusto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    NumeroCentroCusto = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SuperiorImediato = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EmailSuperior = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Divisoes = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    NewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnoBaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planilhas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planilhas_AnoBases_AnoBaseId",
                        column: x => x.AnoBaseId,
                        principalTable: "AnoBases",
                        principalColumn: "AnoBaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AzureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Perfil = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EmailSuperior = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EhDiretor = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    NewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnoBaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_AnoBases_AnoBaseId",
                        column: x => x.AnoBaseId,
                        principalTable: "AnoBases",
                        principalColumn: "AnoBaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Planilhas_AnoBaseId",
                table: "Planilhas",
                column: "AnoBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AnoBaseId",
                table: "Usuarios",
                column: "AnoBaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planilhas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "AnoBases");
        }
    }
}
