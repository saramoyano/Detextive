using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class revisada2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProy",
                table: "EtiquetaSet",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProy",
                table: "EtiquetaSet");
        }
    }
}
