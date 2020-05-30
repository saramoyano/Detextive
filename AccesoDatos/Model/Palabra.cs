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
        private int _idProy;
        private int _idNube;
        private int _numApariciones;
        private float? _porcentaje;
        private int? _numPalabrasVinculadas;
        private Proyecto _proyecto;
        private Nube _nube;

        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int IdProy { get => _idProy; set => _idProy = value; }
        public int NumApariciones { get => _numApariciones; set => _numApariciones = value; }
        public float? Porcentaje { get => _porcentaje; set => _porcentaje = value; }
        public int? NumPalabrasVinculadas { get => _numPalabrasVinculadas; set => _numPalabrasVinculadas = value; }
        public int IdNube { get => _idNube; set => _idNube = value; }
        public Proyecto Proyecto { get => _proyecto; set => _proyecto = value; }
        public Nube Nube { get => _nube; set => _nube = value; }
    }
}
