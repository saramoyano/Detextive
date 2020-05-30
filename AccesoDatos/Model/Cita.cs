using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Cita : NotifyBase
    {
        private int _id;
        private int _idEtiqueta;
        private int _idDoc;

        private string _texto;
        private Etiqueta _etiqueta;
        private Documento _documento;

        
        public int Id { get => _id; set => _id = value; }
        public int IdEtiqueta { get => _idEtiqueta; set => _idEtiqueta = value; }
        public int IdDoc { get => _idDoc; set => _idDoc = value; }
        public string Texto { get => _texto; set { _texto = value; NotificarCambio("Texto"); } }

        public Etiqueta Etiqueta { get => _etiqueta; set => _etiqueta = value; }
        public Documento Documento { get => _documento; set => _documento = value; }
    }
}
