using AccesoDatos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AccesoDatos.Logica
{
    public class LogicaEtiqueta
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

        // Obtener la lista de Etiquetas
        // params: ninguno
        public ObservableCollection<Model.Etiqueta> ListaEtiquetas()
        {

            ObservableCollection<Etiqueta> etiquetas = new ObservableCollection<Etiqueta>();
            using (var db = new Model.Context())
            {
                
                try
                {
                     List<Etiqueta> etiq =  db.EtiquetaSet.OrderBy(b => b.Id).ToList();
                    foreach (Etiqueta etiqueta in etiq) {
                        etiquetas.Add(etiqueta);                       
                    }
                    return etiquetas;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay etiquetas guardadas.", e);
                }

            }
        }

        public List<Model.Etiqueta> ListaEtiquetasList()
        {
            ObservableCollection<Etiqueta> etiquetas = new ObservableCollection<Etiqueta>();
            using (var db = new Model.Context())
            {

                try
                {
                    return  db.EtiquetaSet.OrderBy(b => b.Id).ToList();
                    
                }
                catch (Exception e)
                {
                    throw new Exception("No hay etiquetas guardadas.", e);
                }

            }

        }

        // Obtener una Etiqueta de la lista según filtro aplicado (proyecto)
        // params: objeto de la clase AccesoDatos.Model.Etiqueta
        public ObservableCollection<Model.Etiqueta> ListaEtiquetasFiltro(Proyecto proyecto)
        {
            using (var db = new Model.Context())
            {
                ObservableCollection<Etiqueta> etiquetas = new ObservableCollection<Etiqueta>();
                try
                {
                    //List<Etiqueta> etiq = db.EtiquetaSet.Where(b => b.IdProy.Equals(proyecto.Id)).ToList();
                    List<Etiqueta> etiq = db.EtiquetaSet.Where(b => b.IdProy.Equals(proyecto.Id)).ToList();
                    foreach (Etiqueta eti in etiq)
                    {
                        etiquetas.Add(eti);
                    }
                    return etiquetas;                  
                }
                catch (Exception e)
                {
                    throw new Exception("No hay etiquetas guardadas.", e);
                }
            }
        }

        //Determina si el documento existe
        //params Objeto tipo documento(propiedad Nombre)
        public bool ExisteEtiqueta(Etiqueta eti, Proyecto p)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    List<Etiqueta> lEtiq = new List<Etiqueta>();
                    lEtiq = db.EtiquetaSet.Where(b => b.IdProy.Equals(p.Id) && b.Nombre.Equals(eti.Nombre)).ToList();
                    if (lEtiq.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Hubo un error en base de datos.", e);
                }
            }
        }

        // Agregar una etiqueta a la lista 
        // params: objeto de la clase AccesoDatos.Model.Etiqueta
        public void AgregarEtiqueta(Model.Etiqueta etiqueta)
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

        // Actualiza una etiqueta de la lista 
        // params: objeto de la clase AccesoDatos.Model.Etiqueta con los datos actualizados
        public void ActualizarEtiqueta(Model.Etiqueta etiqueta)
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


        // Elimina un etiqueta de la lista 
        // params: objeto de la clase AccesoDatos.Model.Etiqueta 
        public void EliminarEtiqueta(Model.Etiqueta etiqueta)
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
