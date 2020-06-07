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
        private Documento _documento;
        private Etiqueta _etiqueta;
        
        
        public int Id { get => _id; set { _id = value; NotificarCambio("Id"); } }      
        public string Texto { get => _texto; set { _texto = value; NotificarCambio("Texto"); } }
        public int EtiquetaId { get => _etiquetaId; set { _etiquetaId = value; NotificarCambio("EtiquetaId"); } }
        public int DocumentoId { get => _documentoId; set { _documentoId = value; NotificarCambio("DocumentoId"); } }

        public Documento Documento { get => _documento; set => _documento = value; }
        public Etiqueta Etiqueta { get => _etiqueta; set => _etiqueta = value; }
    }
}
