using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class finalcorregida2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPalabrasVinculadas",
                table: "PalabraSet");

            migrationBuilder.DropColumn(
                name: "Porcentaje",
                table: "PalabraSet");

            migrationBuilder.DropColumn(
                name: "NumDocumentos",
                table: "NubeSet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumPalabrasVinculadas",
                table: "PalabraSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Porcentaje",
                table: "PalabraSet",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumDocumentos",
                table: "NubeSet",
                type: "text",
                nullable: true);
        }
    }
}
