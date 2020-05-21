using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Logica
{
    class LogicaEtiqueta
    {
        private static LogicaEtiqueta logicaEtiqueta;

        public static LogicaEtiqueta GetInstance()
        {
            if (logicaEtiqueta == null)
            {
                logicaEtiqueta = new LogicaEtiqueta();
            }
            return logicaEtiqueta;
        }


        public List<Model.Etiqueta> ListaEtiquetas()
        {
            using (var db = new Model.Context())
            {

                try
                {
                    return db.EtiquetaSet.OrderBy(b => b.Id).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("No hay etiquetas guardadas.", e);
                }

            }

        }

        // Agregar un etiqueta a la lista 
        // params: objeto de la clase AccesoDatos.Model.Etiqueta
        public void AgregarEtiqueta(Etiqueta etiqueta)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    db.EtiquetaSet.Add(etiqueta);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido agregar la etiqueta ", e);
                }
            }

        }

        public void ActualizarEtiqueta(Etiqueta etiqueta)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var etiq = db.EtiquetaSet.FirstOrDefault(x => x.Id == etiqueta.Id);
                    db.Entry(etiq).State = EntityState.Modified;
                    db.Entry(etiq).CurrentValues.SetValues(etiqueta);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido actualizar la etiqueta ", e);

                }
            }
        }

        public void EliminarEtiqueta(Etiqueta etiqueta)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var etiq = db.EtiquetaSet.FirstOrDefault(x => x.Id == etiqueta.Id);
                    db.EtiquetaSet.Remove(etiq);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido eliminar la etiqueta ", e);

                }
            }
        }
    }
}
}
