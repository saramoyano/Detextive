using AccesoDatos.Model;
using Microsoft.EntityFrameworkCore;
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


        public List<Model.Proyecto> ListaProyectos()
        {
            using (var db = new Model.Context())
            {

                try
                {
                    return db.ProyectoSet
                .OrderBy(b => b.Id)
                .ToList();
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