using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Context : DbContext
    {
        public DbSet<Proyecto> ProyectoSet { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proyecto>().Property(b => b.Nombre).IsRequired();
         
            modelBuilder.Entity<Proyecto>().HasIndex(p =>  p.Nombre).IsUnique(true);

            modelBuilder.Entity<Proyecto>().HasMany(b => b.Documentos).WithOne(p => p.Proyecto)
                        .HasForeignKey(p => p.ProyectoId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Proyecto>().HasMany(b => b.Etiquetas).WithOne(p => p.Proyecto)
                        .HasForeignKey(p => p.ProyectoId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Proyecto>().HasMany(b => b.Nubes).WithOne(p => p.Proyecto)
                        .HasForeignKey(p => p.ProyectoId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Nube>().HasMany(b => b.Palabras).WithOne(p => p.Nube)
                        .HasForeignKey(p => p.NubeId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Documento>().HasMany(b => b.Citas).WithOne(p => p.Documento)
                        .HasForeignKey(p => p.DocumentoId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Etiqueta>().HasMany(b => b.Citas).WithOne(p => p.Etiqueta)
                        .HasForeignKey(p => p.EtiquetaId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
        #endregion
        public DbSet<Documento> DocumentoSet { get; set; }       
        public DbSet<Etiqueta> EtiquetaSet { get; set; }
        public DbSet<Nube> NubeSet { get; set; }
        public DbSet<Palabra> PalabraSet { get; set; }
        public DbSet<Cita> CitaSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql(@"host=localhost;port=5432;Database=Detextive;user id=postgres;Password=0000");

    }
}
