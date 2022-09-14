using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Validator.Data.Migrations
{
    public partial class V_1_0_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioAvaliador",
                table: "UsuarioAvaliador");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsuarioAvaliador",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioAvaliador",
                table: "UsuarioAvaliador",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAvaliador_UsuarioId",
                table: "UsuarioAvaliador",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioAvaliador",
                table: "UsuarioAvaliador");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioAvaliador_UsuarioId",
                table: "UsuarioAvaliador");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsuarioAvaliador");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioAvaliador",
                table: "UsuarioAvaliador",
                columns: new[] { "UsuarioId", "AvaliadorId" });
        }
    }
}
