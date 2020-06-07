using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre1",
                table: "DocumentoSet");

            migrationBuilder.AddColumn<int>(
                name: "NubeId",
                table: "DocumentoSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NubeId1",
                table: "DocumentoSet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoSet_NubeId1",
                table: "DocumentoSet",
                column: "NubeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentoSet_NubeSet_NubeId1",
                table: "DocumentoSet",
                column: "NubeId1",
                principalTable: "NubeSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentoSet_NubeSet_NubeId1",
                table: "DocumentoSet");

            migrationBuilder.DropIndex(
                name: "IX_DocumentoSet_NubeId1",
                table: "DocumentoSet");

            migrationBuilder.DropColumn(
                name: "NubeId",
                table: "DocumentoSet");

            migrationBuilder.DropColumn(
                name: "NubeId1",
                table: "DocumentoSet");

            migrationBuilder.AddColumn<string>(
                name: "Nombre1",
                table: "DocumentoSet",
                type: "text",
                nullable: true);
        }
    }
}
