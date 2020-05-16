using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.Model
{
    public class Documento
    {
        private int _id;
        private int _idProy;
        private string _tag;
        private string _nombre;
        private string _ubicacion;

        public int Id { get => _id; set => _id = value; }
        public int IdProy { get => _idProy; set => _idProy = value; }
        public string Tag { get => _tag; set => _tag = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }

        public ICollection<Cita> CitasSet;
        
    }

}
