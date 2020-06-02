using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccesoDatos.Model
{
    public class Palabra
    {
        private int _id;
        private string _nombre;
        private int _proyectoId;
        private int _nubeId;
        private int _numApariciones;
        private float? _porcentaje;
        private int? _numPalabrasVinculadas;        

        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int NumApariciones { get => _numApariciones; set => _numApariciones = value; }
        public float? Porcentaje { get => _porcentaje; set => _porcentaje = value; }
        public int? NumPalabrasVinculadas { get => _numPalabrasVinculadas; set => _numPalabrasVinculadas = value; }
        public int ProyectoId { get => _proyectoId; set => _proyectoId = value; }
        public int NubeId { get => _nubeId; set => _nubeId = value; }
    }
}
