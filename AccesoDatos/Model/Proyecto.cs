using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Proyecto
    {
        private int _id;
        private int? _numPalabras;
        private int? _numEtiquetas;
        private int? _numCitas;
        private string _nombre;
        private string _nombreDocActivo;
        public ICollection<Documento> Documentos;
        public ICollection<Etiqueta> Etiquetas;
        public ICollection<Nube> Nubes;

        public Proyecto() {
            Documentos = new List<Documento>();
            Etiquetas = new List<Etiqueta>();
            Nubes = new List<Nube>();
        }

        public int Id { get => _id; set => _id = value; }
        public int? NumPalabras { get => _numPalabras; set => _numPalabras = value; }
        public int? NumEtiquetas { get => _numEtiquetas; set => _numEtiquetas = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int? NumCitas { get => _numCitas; set => _numCitas = value; }
        public string NombreDocActivo { get => _nombreDocActivo; set => _nombreDocActivo = value; }

      
    }
}
