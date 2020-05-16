using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.Model
{
    public class Etiqueta
    {
        private int _id;
        private string _nombre;
        private int _idProy;
        private int _numCitas;

        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int IdProy { get => _idProy; set => _idProy = value; }
        public int NumCitas { get => _numCitas; set => _numCitas = value; }

        public ICollection<Cita> CitasSet;
        
    }
}
