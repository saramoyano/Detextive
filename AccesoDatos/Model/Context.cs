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
            modelBuilder.Entity<Proyecto>()
                .Property(b => b.Nombre)
                .IsRequired();
         
            modelBuilder.Entity<Proyecto>()
                .HasIndex(p =>  p.Nombre)
                .IsUnique(true);

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
