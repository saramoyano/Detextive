using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Etiqueta : NotifyBase
    {
        private int _id;
        private string _nombre;
        private int _proyectoId;
        private int _numCitas;
        private Proyecto _proyecto;
        private List<Cita> citas;

        public Etiqueta() {
            Citas = new List<Cita>();
        }

        public int Id { get => _id; set { _id = value; NotificarCambio("Id"); } }
        public string Nombre { get { return _nombre; } set { _nombre = value; NotificarCambio("Nombre"); } }
        public int ProyectoId { get => _proyectoId; set { _proyectoId = value; NotificarCambio("Id"); } }
        public int NumCitas { get => _numCitas; set { _numCitas = value; NotificarCambio("NumCitas"); }}
        public Proyecto Proyecto { get => _proyecto; set { _proyecto = value; NotificarCambio("Proyecto"); } }

        public List<Cita> Citas { get => citas; set => citas = value; }
    }
}
