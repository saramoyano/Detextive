using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccesoDatos.Logica
{
    public class LogicaProyecto
    {


        private static LogicaProyecto logicaProyecto;

        public static LogicaProyecto GetInstance()
        {
            if (logicaProyecto == null)
            {
                logicaProyecto = new LogicaProyecto();
            }
            return logicaProyecto;
        }


        public List<Model.Proyecto> ListaProyectos() {
            using (var db = new Model.Context())
            {

                return db.ProyectoSet
                   .OrderBy(b => b.Id)
                   .ToList();
            }

        }

        public void AgregarProyecto() {

            
        }




        
      
}
