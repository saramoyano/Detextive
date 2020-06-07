﻿// <auto-generated />
using System;
using AccesoDatos.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AccesoDatos.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20200604135838_final")]
    partial class final
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AccesoDatos.Model.Cita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DocumentoId")
                        .HasColumnType("integer");

                    b.Property<int>("EtiquetaId")
                        .HasColumnType("integer");

                    b.Property<string>("Texto")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CitaSet");
                });

            modelBuilder.Entity("AccesoDatos.Model.Documento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Extension")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<int>("NubeId")
                        .HasColumnType("integer");

                    b.Property<int?>("NubeId1")
                        .HasColumnType("integer");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("integer");

                    b.Property<string>("Ubicacion")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NubeId1");

                    b.ToTable("DocumentoSet");
                });

            modelBuilder.Entity("AccesoDatos.Model.Etiqueta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<int>("NumCitas")
                        .HasColumnType("integer");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProyectoId");

                    b.ToTable("EtiquetaSet");
                });

            modelBuilder.Entity("AccesoDatos.Model.Nube", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("DocumentoId")
                        .HasColumnType("integer");

                    b.Property<int>("ExtensionFragmento")
                        .HasColumnType("integer");

                    b.Property<int>("NumConceptos")
                        .HasColumnType("integer");

                    b.Property<string>("NumDocumentos")
                        .HasColumnType("text");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("NubeSet");
                });

            modelBuilder.Entity("AccesoDatos.Model.Palabra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<int>("NubeId")
                        .HasColumnType("integer");

                    b.Property<int>("NumApariciones")
                        .HasColumnType("integer");

                    b.Property<int?>("NumPalabrasVinculadas")
                        .HasColumnType("integer");

                    b.Property<float?>("Porcentaje")
                        .HasColumnType("real");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PalabraSet");
                });

            modelBuilder.Entity("AccesoDatos.Model.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NombreDocActivo")
                        .HasColumnType("text");

                    b.Property<int?>("NumCitas")
                        .HasColumnType("integer");

                    b.Property<int?>("NumEtiquetas")
                        .HasColumnType("integer");

                    b.Property<int?>("NumPalabras")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("ProyectoSet");
                });

            modelBuilder.Entity("AccesoDatos.Model.Documento", b =>
                {
                    b.HasOne("AccesoDatos.Model.Nube", "Nube")
                        .WithMany()
                        .HasForeignKey("NubeId1");
                });

            modelBuilder.Entity("AccesoDatos.Model.Etiqueta", b =>
                {
                    b.HasOne("AccesoDatos.Model.Proyecto", "Proyecto")
                        .WithMany()
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
