using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Nube : NotifyBase
    {
        private int _id;
        private int _proyectoId;
        private int? _documentoId;
        private int _numConceptos;
        private int _extensionFragmento;
        private int _numDocumentos;
        private Proyecto _proyecto;
        private List<Palabra> palabras;

        public Nube() {
            Palabras = new List<Palabra>();
        }

        public int Id { get => _id; set { _id = value; NotificarCambio("Id"); }}         
        public int NumConceptos { get => _numConceptos; set { _numConceptos = value; NotificarCambio("NumConceptos"); } }
        public int ExtensionFragmento { get => _extensionFragmento; set { _extensionFragmento = value; NotificarCambio("Extension Fragmento"); }}
        public int ProyectoId { get => _proyectoId; set { _proyectoId = value; NotificarCambio("ProyectoId"); }}
        public int? DocumentoId { get => _documentoId; set { _documentoId = value; NotificarCambio("DocumentoId"); }}
        public int NumDocumentos { get => _numDocumentos; set { _numDocumentos = value; NotificarCambio("NumDocumentos"); } }

        public Proyecto Proyecto { get => _proyecto; set => _proyecto = value; }
        public List<Palabra> Palabras { get => palabras; set => palabras = value; }
    }
}
