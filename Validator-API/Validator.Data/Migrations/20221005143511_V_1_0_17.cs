using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Validator.Data.Migrations
{
    public partial class V_1_0_17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EhGestor",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GestorCorporativo",
                table: "Planilhas",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EhGestor",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "GestorCorporativo",
                table: "Planilhas");
        }
    }
}
