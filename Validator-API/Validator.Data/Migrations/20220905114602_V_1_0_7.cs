using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Validator.Data.Migrations
{
    public partial class V_1_0_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parametro_AnoBases_AnoBaseId",
                table: "Parametro");

            migrationBuilder.CreateTable(
                name: "Processos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DhInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DhFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnoBaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processos_AnoBases_AnoBaseId",
                        column: x => x.AnoBaseId,
                        principalTable: "AnoBases",
                        principalColumn: "AnoBaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processos_AnoBaseId",
                table: "Processos",
                column: "AnoBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parametro_AnoBases_AnoBaseId",
                table: "Parametro",
                column: "AnoBaseId",
                principalTable: "AnoBases",
                principalColumn: "AnoBaseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parametro_AnoBases_AnoBaseId",
                table: "Parametro");

            migrationBuilder.DropTable(
                name: "Processos");

            migrationBuilder.AddForeignKey(
                name: "FK_Parametro_AnoBases_AnoBaseId",
                table: "Parametro",
                column: "AnoBaseId",
                principalTable: "AnoBases",
                principalColumn: "AnoBaseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
