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

        public int Id { get => _id; set => _id = value; }
        public int? NumPalabras { get => _numPalabras; set => _numPalabras = value; }
        public int? NumEtiquetas { get => _numEtiquetas; set => _numEtiquetas = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int? NumCitas { get => _numCitas; set => _numCitas = value; }

        public ICollection<Documento> Documentos;
        public ICollection<Etiqueta> Etiquetas;
        public ICollection<Nube> Nubes;
    }
}
