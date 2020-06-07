using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class finalcorregida4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreDocActivo",
                table: "ProyectoSet");

            migrationBuilder.AddColumn<string>(
                name: "UbicacionDocActivo",
                table: "ProyectoSet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UbicacionDocActivo",
                table: "ProyectoSet");

            migrationBuilder.AddColumn<string>(
                name: "NombreDocActivo",
                table: "ProyectoSet",
                type: "text",
                nullable: true);
        }
    }
}
