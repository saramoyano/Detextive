using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AccesoDatos.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProyectoSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumPalabras = table.Column<int>(nullable: false),
                    NumEtiquetas = table.Column<int>(nullable: false),
                    NumCitas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentoSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProy = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Ubicacion = table.Column<string>(nullable: true),
                    ProyectoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentoSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentoSet_ProyectoSet_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "ProyectoSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EtiquetaSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(nullable: true),
                    IdProy = table.Column<int>(nullable: false),
                    NumCitas = table.Column<int>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtiquetaSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtiquetaSet_ProyectoSet_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "ProyectoSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NubeSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProy = table.Column<int>(nullable: false),
                    NumConceptos = table.Column<int>(nullable: false),
                    ExtensionFragmento = table.Column<int>(nullable: false),
                    NumDocumentos = table.Column<string>(nullable: true),
                    ProyectoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NubeSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NubeSet_ProyectoSet_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "ProyectoSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PalabraSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(nullable: true),
                    IdProy = table.Column<int>(nullable: false),
                    NumApariciones = table.Column<int>(nullable: false),
                    Porcentaje = table.Column<float>(nullable: false),
                    NumPalabrasVinculadas = table.Column<int>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalabraSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PalabraSet_ProyectoSet_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "ProyectoSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CitaSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEtiqueta = table.Column<int>(nullable: false),
                    IdDoc = table.Column<int>(nullable: false),
                    Texto = table.Column<string>(nullable: true),
                    ProyectoId = table.Column<int>(nullable: true),
                    DocumentoId = table.Column<int>(nullable: true),
                    EtiquetaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitaSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitaSet_DocumentoSet_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "DocumentoSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitaSet_EtiquetaSet_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "EtiquetaSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitaSet_ProyectoSet_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "ProyectoSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoSet_ProyectoId",
                table: "DocumentoSet",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_EtiquetaSet_ProyectoId",
                table: "EtiquetaSet",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_NubeSet_ProyectoId",
                table: "NubeSet",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_PalabraSet_ProyectoId",
                table: "PalabraSet",
                column: "ProyectoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitaSet");

            migrationBuilder.DropTable(
                name: "NubeSet");

            migrationBuilder.DropTable(
                name: "PalabraSet");

            migrationBuilder.DropTable(
                name: "DocumentoSet");

            migrationBuilder.DropTable(
                name: "EtiquetaSet");

            migrationBuilder.DropTable(
                name: "ProyectoSet");
        }
    }
}
