using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.Model
{
    public class Palabra
    {
        private int _id;
        private string _nombre;
        private int _idProy;
        private int _numApariciones;
        private float _porcentaje;
        private int _numPalabrasVinculadas;

        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int IdProy { get => _idProy; set => _idProy = value; }
        public int NumApariciones { get => _numApariciones; set => _numApariciones = value; }
        public float Porcentaje { get => _porcentaje; set => _porcentaje = value; }
        public int NumPalabrasVinculadas { get => _numPalabrasVinculadas; set => _numPalabrasVinculadas = value; }
    }
}
