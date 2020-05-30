using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class Cambios4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdNube",
                table: "PalabraSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "NubeSet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "DocumentoSet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoSet_Nombre",
                table: "ProyectoSet",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NubeSet_ProyectoId",
                table: "NubeSet",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_EtiquetaSet_Nombre",
                table: "EtiquetaSet",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoSet_Nombre",
                table: "DocumentoSet",
                column: "Nombre",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentoSet_ProyectoSet_ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropForeignKey(
                name: "FK_NubeSet_ProyectoSet_ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropIndex(
                name: "IX_ProyectoSet_Nombre",
                table: "ProyectoSet");

            migrationBuilder.DropIndex(
                name: "IX_NubeSet_ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropIndex(
                name: "IX_EtiquetaSet_Nombre",
                table: "EtiquetaSet");

            migrationBuilder.DropIndex(
                name: "IX_DocumentoSet_Nombre",
                table: "DocumentoSet");

            migrationBuilder.DropIndex(
                name: "IX_DocumentoSet_ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropColumn(
                name: "IdNube",
                table: "PalabraSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "DocumentoSet");
        }
    }
}
