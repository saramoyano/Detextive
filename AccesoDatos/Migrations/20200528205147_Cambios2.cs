using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class Cambios2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitaSet_DocumentoSet_DocumentoId",
                table: "CitaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_CitaSet_EtiquetaSet_EtiquetaId",
                table: "CitaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_CitaSet_ProyectoSet_ProyectoId",
                table: "CitaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentoSet_ProyectoSet_ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropForeignKey(
                name: "FK_NubeSet_ProyectoSet_ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropForeignKey(
                name: "FK_PalabraSet_ProyectoSet_ProyectoId",
                table: "PalabraSet");

            migrationBuilder.DropIndex(
                name: "IX_PalabraSet_ProyectoId",
                table: "PalabraSet");

            migrationBuilder.DropIndex(
                name: "IX_NubeSet_ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropIndex(
                name: "IX_DocumentoSet_ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropIndex(
                name: "IX_CitaSet_DocumentoId",
                table: "CitaSet");

            migrationBuilder.DropIndex(
                name: "IX_CitaSet_EtiquetaId",
                table: "CitaSet");

            migrationBuilder.DropIndex(
                name: "IX_CitaSet_ProyectoId",
                table: "CitaSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "PalabraSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "CitaSet");

            migrationBuilder.DropColumn(
                name: "EtiquetaId",
                table: "CitaSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "CitaSet");

            migrationBuilder.AddColumn<int>(
                name: "IdDoc",
                table: "NubeSet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdDoc",
                table: "NubeSet");

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "PalabraSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "NubeSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "DocumentoSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "CitaSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EtiquetaId",
                table: "CitaSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "CitaSet",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PalabraSet_ProyectoId",
                table: "PalabraSet",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_NubeSet_ProyectoId",
                table: "NubeSet",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoSet_ProyectoId",
                table: "DocumentoSet",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_CitaSet_DocumentoId",
                table: "CitaSet",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CitaSet_EtiquetaId",
                table: "CitaSet",
                column: "EtiquetaId");

            migrationBuilder.CreateIndex(
                name: "IX_CitaSet_ProyectoId",
                table: "CitaSet",
                column: "ProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CitaSet_DocumentoSet_DocumentoId",
                table: "CitaSet",
                column: "DocumentoId",
                principalTable: "DocumentoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CitaSet_EtiquetaSet_EtiquetaId",
                table: "CitaSet",
                column: "EtiquetaId",
                principalTable: "EtiquetaSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CitaSet_ProyectoSet_ProyectoId",
                table: "CitaSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_PalabraSet_ProyectoSet_ProyectoId",
                table: "PalabraSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
