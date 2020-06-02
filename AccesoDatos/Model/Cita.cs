using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Cita : NotifyBase
    {
        private int _id;
        private int _etiquetaId;
        private int _documentoId;
        private string _texto;
        
        
        public int Id { get => _id; set => _id = value; }      
        public string Texto { get => _texto; set { _texto = value; NotificarCambio("Texto"); } }
        public int EtiquetaId { get => _etiquetaId; set => _etiquetaId = value; }
        public int DocumentoId { get => _documentoId; set => _documentoId = value; }
    }
}
