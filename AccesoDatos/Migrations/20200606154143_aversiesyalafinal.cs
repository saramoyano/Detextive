using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class aversiesyalafinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PalabraSet_NubeId",
                table: "PalabraSet",
                column: "NubeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CitaSet_DocumentoSet_DocumentoId",
                table: "CitaSet",
                column: "DocumentoId",
                principalTable: "DocumentoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitaSet_EtiquetaSet_EtiquetaId",
                table: "CitaSet",
                column: "EtiquetaId",
                principalTable: "EtiquetaSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentoSet_ProyectoSet_ProyectoId",
                table: "DocumentoSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NubeSet_ProyectoSet_ProyectoId",
                table: "NubeSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PalabraSet_NubeSet_NubeId",
                table: "PalabraSet",
                column: "NubeId",
                principalTable: "NubeSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PalabraSet_ProyectoSet_ProyectoId",
                table: "PalabraSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitaSet_DocumentoSet_DocumentoId",
                table: "CitaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_CitaSet_EtiquetaSet_EtiquetaId",
                table: "CitaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentoSet_ProyectoSet_ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropForeignKey(
                name: "FK_NubeSet_ProyectoSet_ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropForeignKey(
                name: "FK_PalabraSet_NubeSet_NubeId",
                table: "PalabraSet");

            migrationBuilder.DropForeignKey(
                name: "FK_PalabraSet_ProyectoSet_ProyectoId",
                table: "PalabraSet");

            migrationBuilder.DropIndex(
                name: "IX_PalabraSet_NubeId",
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
        }
    }
}
