using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Model
{
    public class Cita
    {
        private int _id;
        private int _idEtiqueta;
        private int _idDoc;
        private string _texto;
        private Proyecto _proyecto;
        private Documento _documento;
        private Etiqueta _etiqueta;

        public int Id { get => _id; set => _id = value; }
        public int IdEtiqueta { get => _idEtiqueta; set => _idEtiqueta = value; }
        public int IdDoc { get => _idDoc; set => _idDoc = value; }
        public string Texto { get => _texto; set => _texto = value; }
        public Proyecto Proyecto { get => _proyecto; set => _proyecto = value; }
        public Documento Documento { get => _documento; set => _documento = value; }
        public Etiqueta Etiqueta { get => _etiqueta; set => _etiqueta = value; }
    }
}
