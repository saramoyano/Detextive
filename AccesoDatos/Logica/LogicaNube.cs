using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Logica
{
    class LogicaNube
    {
        private static LogicaNube logicaNube;

        public static LogicaNube GetInstance()
        {
            if (logicaNube == null)
            {
                logicaNube = new LogicaNube();
            }
            return logicaNube;
        }


        public List<Model.Nube> ListaNubes()
        {
            using (var db = new Model.Context())
            {

                try
                {
                    return db.NubeSet.OrderBy(b => b.Id).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("No hay nubes guardadas.", e);
                }

            }

        }

        // Agregar un nube a la lista 
        // params: objeto de la clase AccesoDatos.Model.Nube
        public void AgregarNube(Nube nube)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    db.NubeSet.Add(nube);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido agregar la nube ", e);
                }
            }

        }

        public void ActualizarNube(Nube nube)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var nubeTmp = db.NubeSet.FirstOrDefault(x => x.Id == nube.Id);
                    db.Entry(nubeTmp).State = EntityState.Modified;
                    db.Entry(nubeTmp).CurrentValues.SetValues(nube);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido actualizar la nube ", e);

                }
            }
        }

        public void EliminarNube(Nube nube)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var nubeTmp = db.NubeSet.FirstOrDefault(x => x.Id == nube.Id);
                    db.NubeSet.Remove(nubeTmp);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido eliminar la nube ", e);

                }
            }
        }
    }
}
