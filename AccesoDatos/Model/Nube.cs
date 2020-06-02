using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Nube
    {
        private int _id;
        private int _proyectoId;
        private int? _documentoId;
        private int _numConceptos;
        private int _extensionFragmento;
        private string _numDocumentos;
        public ICollection<Palabra> Palabras;

        public Nube() {
            Palabras = new List<Palabra>();
        }

        public int Id { get => _id; set => _id = value; }         
        public int NumConceptos { get => _numConceptos; set => _numConceptos = value; }
        public int ExtensionFragmento { get => _extensionFragmento; set => _extensionFragmento = value; }
        public string NumDocumentos { get => _numDocumentos; set => _numDocumentos = value; }
        public int ProyectoId { get => _proyectoId; set => _proyectoId = value; }
        public int? DocumentoId { get => _documentoId; set => _documentoId = value; }

       

    }
}
