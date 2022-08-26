using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Validator.Data.Migrations
{
    public partial class V_1_0_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewId",
                table: "Planilhas");

            migrationBuilder.DropColumn(
                name: "NewId",
                table: "AnoBases");

            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "Usuarios",
                newName: "SuperiorId");

            migrationBuilder.AlterColumn<int>(
                name: "Perfil",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DivisaoId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SetorId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Divisao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AnoBaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Divisao_AnoBases_AnoBaseId",
                        column: x => x.AnoBaseId,
                        principalTable: "AnoBases",
                        principalColumn: "AnoBaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parametro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QtdeSugestaoMin = table.Column<int>(type: "int", nullable: false),
                    QtdeSugestaoMax = table.Column<int>(type: "int", nullable: false),
                    QtdeAvaliador = table.Column<int>(type: "int", nullable: false),
                    AnoBaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parametro_AnoBases_AnoBaseId",
                        column: x => x.AnoBaseId,
                        principalTable: "AnoBases",
                        principalColumn: "AnoBaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AnoBaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setor_AnoBases_AnoBaseId",
                        column: x => x.AnoBaseId,
                        principalTable: "AnoBases",
                        principalColumn: "AnoBaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAvaliador",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvaliadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAvaliador", x => new { x.UsuarioId, x.AvaliadorId });
                    table.ForeignKey(
                        name: "FK_UsuarioAvaliador_Usuarios_AvaliadorId",
                        column: x => x.AvaliadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioAvaliador_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DivisaoId",
                table: "Usuarios",
                column: "DivisaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SetorId",
                table: "Usuarios",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SuperiorId",
                table: "Usuarios",
                column: "SuperiorId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisao_AnoBaseId",
                table: "Divisao",
                column: "AnoBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Parametro_AnoBaseId",
                table: "Parametro",
                column: "AnoBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_AnoBaseId",
                table: "Setor",
                column: "AnoBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAvaliador_AvaliadorId",
                table: "UsuarioAvaliador",
                column: "AvaliadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Divisao_DivisaoId",
                table: "Usuarios",
                column: "DivisaoId",
                principalTable: "Divisao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Setor_SetorId",
                table: "Usuarios",
                column: "SetorId",
                principalTable: "Setor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_SuperiorId",
                table: "Usuarios",
                column: "SuperiorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Divisao_DivisaoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Setor_SetorId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_SuperiorId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Divisao");

            migrationBuilder.DropTable(
                name: "Parametro");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropTable(
                name: "UsuarioAvaliador");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_DivisaoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SetorId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SuperiorId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DivisaoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SetorId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "SuperiorId",
                table: "Usuarios",
                newName: "NewId");

            migrationBuilder.AlterColumn<int>(
                name: "Perfil",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "NewId",
                table: "Planilhas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NewId",
                table: "AnoBases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
