using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Documento : NotifyBase
    {
        private int _id;
        private int _proyectoId;         
        private string _nombre;
        private string _ubicacion;
        private int _extension;
        public ICollection<Cita> Citas;

        public Documento() {
            Citas = new List<Cita>();
        }

        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => Nombre1;  set { Nombre1 = value; NotificarCambio("Nombre"); } }
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        public int Extension { get => _extension; set => _extension = value; }
        public string Nombre1 { get => _nombre; set => _nombre = value; }
        public int ProyectoId { get => _proyectoId; set => _proyectoId = value; 
        }
    }
}
