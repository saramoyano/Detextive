using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class Cambios5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentoSet_ProyectoSet_ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropForeignKey(
                name: "FK_NubeSet_ProyectoSet_ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropIndex(
                name: "IX_NubeSet_ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropIndex(
                name: "IX_DocumentoSet_ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropColumn(
                name: "IdProy",
                table: "EtiquetaSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "DocumentoSet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "NubeSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProy",
                table: "EtiquetaSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "DocumentoSet",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NubeSet_ProyectoId",
                table: "NubeSet",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoSet_ProyectoId",
                table: "DocumentoSet",
                column: "ProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentoSet_ProyectoSet_ProyectoId",
                table: "DocumentoSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NubeSet_ProyectoSet_ProyectoId",
                table: "NubeSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
