using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Proyecto : NotifyBase
    {
        private int _id;       
        private int? _numEtiquetas;
        private int? _numDocumentos;
        private string _nombre;
        private string _ubicacionDocActivo;
        private List<Documento> documentos;
        private List<Etiqueta> etiquetas;
        private List<Nube> nubes;

        public Proyecto() {
            Documentos = new List<Documento>();
            Etiquetas = new List<Etiqueta>();
            Nubes = new List<Nube>();
        }
        public int Id { get => _id; set { _id = value; NotificarCambio("Id"); }}
        public int? NumEtiquetas { get => _numEtiquetas; set { _numEtiquetas = value; NotificarCambio("NumEtiquetas"); }}
        public string Nombre { get => _nombre; set { _nombre = value; NotificarCambio("Nombre"); }}
        public int? NumDocumentos { get => _numDocumentos; set { _numDocumentos = value; NotificarCambio("numDocumentos"); }}
        public string UbicacionDocActivo { get => _ubicacionDocActivo; set { _ubicacionDocActivo = value; NotificarCambio("UbicacionDocActivo"); }}

        public List<Documento> Documentos { get => documentos; set => documentos = value; }
        public List<Etiqueta> Etiquetas { get => etiquetas; set => etiquetas = value; }
        public List<Nube> Nubes { get => nubes; set => nubes = value; }
    }
}
