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
        
        public int Id { get => _id; set => _id = value; }
        public int IdEtiqueta { get => _idEtiqueta; set => _idEtiqueta = value; }
        public int IdDoc { get => _idDoc; set => _idDoc = value; }
        public string Texto { get => _texto; set { _texto = value; NotificarCambio("Texto"); } }

    }
}
