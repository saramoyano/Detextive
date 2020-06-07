using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccesoDatos.Model
{
    public class Palabra : NotifyBase
    {
        private int _id;
        private string _nombre;
        private int _proyectoId;
        private int _nubeId;
        private int _numApariciones;
        private Nube _nube;
        private Proyecto _proyecto;

        public int Id { get => _id; set { _id = value; NotificarCambio("Id"); }}
        public string Nombre { get => _nombre; set { _nombre = value; NotificarCambio("Nombre"); }}
        public int NumApariciones { get => _numApariciones; set { _numApariciones = value; NotificarCambio("NumApariciones"); } }
        public int ProyectoId { get => _proyectoId; set { _proyectoId = value; NotificarCambio("ProyectoId"); }}
        public int NubeId { get => _nubeId; set { _nubeId = value; NotificarCambio("NubeId"); } }
        public Nube Nube { get => _nube; set => _nube = value; }
        public Proyecto Proyecto { get => _proyecto; set => _proyecto = value; }
    }
}
