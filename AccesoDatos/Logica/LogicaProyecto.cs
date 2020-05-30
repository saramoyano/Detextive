using AccesoDatos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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

        // Obtener la lista de Proyectos
        // params: ninguno
        public ObservableCollection<Model.Proyecto> ListaProyectos()
        {
            using (var db = new Model.Context())
            {
                ObservableCollection<Proyecto> proyectos = new ObservableCollection<Proyecto>();
                try
                {
                    var lsProy =  db.ProyectoSet.OrderBy(b => b.Id).ToList();
                    foreach (Proyecto p in lsProy) {
                        proyectos.Add(p);
                    }
                    return proyectos;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay proyectos guardados.", e);
                }

            }

        }

        // Obtener un Proyecto de la lista según filtro aplicado (propiedad nombre)
        // params: objeto de la clase AccesoDatos.Model.Proyecto
        public bool ExisteProyecto(Proyecto proyecto)
        {
            using (var db = new Model.Context())
            {

                try
                {
                    Proyecto proy = db.ProyectoSet.Single(b => b.Nombre.Equals(proyecto.Nombre));
                    if (proy == null) {
                        return false;
                    }
                    else {
                        return true; }
                }
                catch (Exception e)
                {
                    throw new Exception("No hay proyectos guardados.", e);
                }
            }
        }

        // Agregar un proyecto a la lista 
        // params: objeto de la clase AccesoDatos.Model.Proyecto
        public void AgregarProyecto(Proyecto proyecto)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    db.ProyectoSet.Add(proyecto);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido agregar el proyecto ", e);
                }
            }

        }

        // Actualiza un proyecto de la lista 
        // params: objeto de la clase AccesoDatos.Model.Proyecto con los datos actualizados
        public void ActualizarProyecto(Proyecto proyecto)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var proy = db.ProyectoSet.FirstOrDefault(x => x.Id == proyecto.Id);
                    db.Entry(proy).State = EntityState.Modified;
                    db.Entry(proy).CurrentValues.SetValues(proyecto);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido actualizar el proyecto ", e);

                }
            }
        }


        // Elimina un proyecto de la lista 
        // params: objeto de la clase AccesoDatos.Model.Proyecto 
        public void EliminarProyecto(Proyecto proyecto)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var proy = db.ProyectoSet.FirstOrDefault(x => x.Id == proyecto.Id);
                    db.ProyectoSet.Remove(proy);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido eliminar el proyecto ", e);

                }
            }
        }
    }
}
