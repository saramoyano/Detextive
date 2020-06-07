using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class finalcorregida3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumCitas",
                table: "ProyectoSet");

            migrationBuilder.DropColumn(
                name: "NumPalabras",
                table: "ProyectoSet");

            migrationBuilder.AddColumn<int>(
                name: "NumDocumentos",
                table: "ProyectoSet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumDocumentos",
                table: "NubeSet",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumDocumentos",
                table: "ProyectoSet");

            migrationBuilder.DropColumn(
                name: "NumDocumentos",
                table: "NubeSet");

            migrationBuilder.AddColumn<int>(
                name: "NumCitas",
                table: "ProyectoSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumPalabras",
                table: "ProyectoSet",
                type: "integer",
                nullable: true);
        }
    }
}
