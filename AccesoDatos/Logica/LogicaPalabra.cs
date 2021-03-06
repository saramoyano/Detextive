﻿using AccesoDatos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AccesoDatos.Logica
{
    public class LogicaPalabra
    {


        private static LogicaPalabra logicaPalabra;

        public static LogicaPalabra GetInstance()
        {
            if (logicaPalabra == null)
            {
                logicaPalabra = new LogicaPalabra();
            }
            return logicaPalabra;
        }

        // Obtener la lista de Palabras
        // params: ninguno
        public ObservableCollection<Model.Palabra> ListaPalabras(Proyecto p)
        {
            using (var db = new Model.Context())
            {
                ObservableCollection<Palabra> palabras = new ObservableCollection<Palabra>();
                try
                {
                    List<Palabra> lPal = db.PalabraSet.Where(b => b.ProyectoId.Equals(p.Id)).ToList();
                    foreach (Palabra pal in lPal)
                    {
                        palabras.Add(pal);
                    }
                    return palabras;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay palabras guardadas.", e);
                }

            }

        }

        public ObservableCollection<Model.Palabra> ListaPalabrasFiltro(Nube n)
        {
            using (var db = new Model.Context())
            {
                ObservableCollection<Palabra> palabras = new ObservableCollection<Palabra>();
                try
                {
                    List<Palabra> lPal = db.PalabraSet.Where(b => b.NubeId.Equals(n.Id)).ToList();
                    foreach (Palabra p in lPal)
                    {
                        palabras.Add(p);
                    }
                    return palabras;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay palabras guardadas.", e);
                }

            }

        }

        // Obtener una Palabra de la lista según filtro aplicado (propiedad nombre)
        // params: objeto de la clase AccesoDatos.Model.Palabra
        public Model.Palabra ListaPalabrasFiltro(Palabra palabra)
        {
            using (var db = new Model.Context())
            {

                try
                {
                    var pal = db.PalabraSet.Single(b => b.Nombre.Equals(palabra.Nombre));
                    return pal;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay palabras guardados.", e);
                }

            }

        }

        public bool ExistePalabra(Palabra pal, Proyecto p)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    List<Palabra> lPal = new List<Palabra>();
                    lPal = db.PalabraSet.Where(b => b.ProyectoId.Equals(p.Id) && b.Nombre.Equals(pal.Nombre)).ToList();
                    if (lPal.Count > 0)
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

        // Agregar una palabra a la lista 
        // params: objeto de la clase AccesoDatos.Model.Palabra
        public void AgregarPalabra(Palabra palabra)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    db.PalabraSet.Add(palabra);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido agregar la palabra ", e);
                }
            }

        }

        // Actualiza una palabra de la lista 
        // params: objeto de la clase AccesoDatos.Model.Palabra con los datos actualizados
        public void ActualizarPalabra(Palabra palabra)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var pal = db.PalabraSet.FirstOrDefault(x => x.Id == palabra.Id);
                    db.Entry(pal).State = EntityState.Modified;
                    db.Entry(pal).CurrentValues.SetValues(palabra);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido actualizar la palabra ", e);

                }
            }
        }


        // Elimina una palabra de la lista 
        // params: objeto de la clase AccesoDatos.Model.Palabra 
        public void EliminarPalabra(Palabra palabra)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var pal = db.PalabraSet.FirstOrDefault(x => x.Id == palabra.Id);
                    db.PalabraSet.Remove(pal);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido eliminar la palabra ", e);

                }
            }
        }
    }
}
