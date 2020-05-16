using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.Model
{
    public class Nube
    {
        private int _id;
        private int _idProy;
        private int _numConceptos;
        private int _extensionFragmento;
        private string _numDocumentos;

        public int Id { get => _id; set => _id = value; }
        public int IdProy { get => _idProy; set => _idProy = value; }
        public int NumConceptos { get => _numConceptos; set => _numConceptos = value; }
        public int ExtensionFragmento { get => _extensionFragmento; set => _extensionFragmento = value; }
        public string NumDocumentos { get => _numDocumentos; set => _numDocumentos = value; }

        public ICollection<Palabra> PalabrasSet;

    }
}
