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
        private int _nubeId;
        private Nube _nube;
        private Proyecto _proyecto;
        private List<Cita> citas;

        public Documento() {
            Citas = new List<Cita>();
        }

        public int Id { get => _id; set { _id = value; NotificarCambio("Id"); } }
        public string Ubicacion { get => _ubicacion; set { _ubicacion = value; NotificarCambio("Ubicacion"); } }
        public int Extension { get => _extension; set { _extension = value; NotificarCambio("Extension"); }}
        public int ProyectoId { get => _proyectoId; set { _proyectoId = value; NotificarCambio("ProyectoId"); }}
        public int NubeId { get => _nubeId; set { _nubeId = value; NotificarCambio("NubeId"); } }
        public Nube Nube { get => _nube; set { _nube = value; NotificarCambio("Nube"); } }
        public string Nombre { get => _nombre; set { _nombre = value; NotificarCambio("Nombre"); }}

        public Proyecto Proyecto { get => _proyecto; set => _proyecto = value; }
        public List<Cita> Citas { get => citas; set => citas = value; }
    }
}
