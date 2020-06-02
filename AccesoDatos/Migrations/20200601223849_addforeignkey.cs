using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class addforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNube",
                table: "PalabraSet");

            migrationBuilder.DropColumn(
                name: "IdProy",
                table: "PalabraSet");

            migrationBuilder.DropColumn(
                name: "IdDoc",
                table: "NubeSet");

            migrationBuilder.DropColumn(
                name: "IdProy",
                table: "NubeSet");

            migrationBuilder.DropColumn(
                name: "IdProy",
                table: "DocumentoSet");

            migrationBuilder.DropColumn(
                name: "IdDoc",
                table: "CitaSet");

            migrationBuilder.DropColumn(
                name: "IdEtiqueta",
                table: "CitaSet");

            migrationBuilder.AddColumn<int>(
                name: "NubeId",
                table: "PalabraSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "PalabraSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "NubeSet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "NubeSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre1",
                table: "DocumentoSet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "DocumentoSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "CitaSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EtiquetaId",
                table: "CitaSet",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NubeId",
                table: "PalabraSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "PalabraSet");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "NubeSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "NubeSet");

            migrationBuilder.DropColumn(
                name: "Nombre1",
                table: "DocumentoSet");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "DocumentoSet");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "CitaSet");

            migrationBuilder.DropColumn(
                name: "EtiquetaId",
                table: "CitaSet");

            migrationBuilder.AddColumn<int>(
                name: "IdNube",
                table: "PalabraSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdProy",
                table: "PalabraSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDoc",
                table: "NubeSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProy",
                table: "NubeSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdProy",
                table: "DocumentoSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDoc",
                table: "CitaSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEtiqueta",
                table: "CitaSet",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
