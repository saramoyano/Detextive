using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class estasieslafinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UbicacionDocActivo",
                table: "ProyectoSet");

            migrationBuilder.AddColumn<string>(
                name: "NombreDocActivo",
                table: "ProyectoSet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "ProyectoSet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreDocActivo",
                table: "ProyectoSet");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "ProyectoSet");

            migrationBuilder.AddColumn<string>(
                name: "UbicacionDocActivo",
                table: "ProyectoSet",
                type: "text",
                nullable: true);
        }
    }
}
