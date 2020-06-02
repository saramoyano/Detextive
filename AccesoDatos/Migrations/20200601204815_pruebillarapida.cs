using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class pruebillarapida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtiquetaSet_ProyectoSet_ProyectoId",
                table: "EtiquetaSet");

            migrationBuilder.DropColumn(
                name: "IdProy",
                table: "EtiquetaSet");

            migrationBuilder.AlterColumn<int>(
                name: "ProyectoId",
                table: "EtiquetaSet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EtiquetaSet_ProyectoSet_ProyectoId",
                table: "EtiquetaSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtiquetaSet_ProyectoSet_ProyectoId",
                table: "EtiquetaSet");

            migrationBuilder.AlterColumn<int>(
                name: "ProyectoId",
                table: "EtiquetaSet",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "IdProy",
                table: "EtiquetaSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EtiquetaSet_ProyectoSet_ProyectoId",
                table: "EtiquetaSet",
                column: "ProyectoId",
                principalTable: "ProyectoSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
