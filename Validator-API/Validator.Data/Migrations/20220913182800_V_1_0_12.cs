using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Validator.Data.Migrations
{
    public partial class V_1_0_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Usuarios",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Usuarios");
        }
    }
}
