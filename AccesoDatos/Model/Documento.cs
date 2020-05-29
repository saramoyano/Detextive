using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Model
{
    public class Documento : NotifyBase
    {
        private int _id;
        private int _idProy;         
        private string _nombre;
        private string _ubicacion;
        private int _extension;
        


        public int Id { get => _id; set => _id = value; }
     
        public int IdProy { get => _idProy; set => _idProy = value; }

        public string Nombre { get { return _nombre; } set { _nombre = value; NotificarCambio("Nombre"); } }

        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        public int Extension { get => _extension; set => _extension = value; }

        public ICollection<Cita> CitasSet;
     

    }
}
