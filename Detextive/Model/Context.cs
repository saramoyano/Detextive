using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.Model
{
    public class Context : DbContext
    {
        public DbSet<Proyecto> ProyectoSet { get; set; }
        public DbSet<Documento> DocumentoSet { get; set; }

        public DbSet<Etiqueta> EtiquetaSet { get; set; }
        public DbSet<Nube> NubeSet { get; set; }
        public DbSet<Palabra> PalabraSet { get; set; }

        public DbSet<Cita> CitaSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
    }
}
