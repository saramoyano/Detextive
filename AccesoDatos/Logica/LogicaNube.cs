using AccesoDatos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AccesoDatos.Logica
{
    public class LogicaNube
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

        // Obtener la lista de nubes
        // params: ninguno
        public ObservableCollection<Model.Nube> ListaNubes()
        {
            using (var db = new Model.Context())
            {
                ObservableCollection<Model.Nube> nubes = new ObservableCollection<Nube>();
                try
                {
                    var lsNubes = db.NubeSet.OrderBy(b => b.Id).ToList();
                    foreach (Nube n in lsNubes)
                    {
                        nubes.Add(n);
                    }
                    return nubes;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay nubes guardadas.", e);
                }

            }

        }


        // Obtener lista de nubes según filtro aplicado  
        // params: objeto de la clase AccesoDatos.Model.nube
        public ObservableCollection<Model.Nube> ListaNubeFiltro(Proyecto proyecto)
        {
            using (var db = new Model.Context())
            {
                ObservableCollection<Model.Nube> nubes = new ObservableCollection<Nube>();
                try
                {
                    var lsNubes = db.NubeSet.Where(b => b.ProyectoId.Equals(proyecto.Id));
                    foreach (Nube n in lsNubes)
                    {
                        nubes.Add(n);
                    }
                    return nubes;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay nubes guardadas.", e);
                }
            }
        }

        public Model.Nube NubeFiltro(Documento d, Proyecto proyecto)
        {
            using (var db = new Model.Context())
            {
                ObservableCollection<Model.Nube> nubes = new ObservableCollection<Nube>();
                try
                {
                    return db.NubeSet.Single(b => b.ProyectoId.Equals(proyecto.Id) && b.DocumentoId.Equals(d.Id));
                    
                }
                catch (Exception e)
                {
                    throw new Exception("No hay nubes guardadas.", e);
                }
            }
        }


        public bool ExisteNube(Documento d, Proyecto p)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    List<Nube> lNubes = new List<Nube>();
                    lNubes = db.NubeSet.Where(b => b.ProyectoId.Equals(p.Id) && b.DocumentoId.Equals(d.Id)).ToList();
                    if (lNubes.Count > 0)
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


        // Agregar una nube a la lista 
        // params: objeto de la clase AccesoDatos.Model.Nube
        public void AgregarNube(Model.Nube nube)
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


        // Actualiza una nube de la lista 
        // params: objeto de la clase AccesoDatos.Model.nube con los datos actualizados
        public void ActualizarNube(Model.Nube nube)
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

        // Elimina una nube de la lista 
        // params: objeto de la clase AccesoDatos.Model.nube 
        public void EliminarNube(Model.Nube nube)
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

        //public static IBlacklist CreateBlacklist(bool excludeEnglishCommonWords)
        //{
        //    return excludeEnglishCommonWords
        //               ? (IBlacklist)new CommonWords()
        //               : new NullBlacklist();
        //}

        //public static IEnumerable<string> CreateExtractor(String texto)
        //{

        //    //   FileInfo fileInfo = new FileInfo(input);
        //    // return new FileExtractor(fileInfo);
        //    return new StringExtractor(texto);
        //}

        //public void ProcessText(string texto)
        //{

        //    string textoNuevo = texto.RemoveStopWords("es");

        //    IEnumerable<string> terms = new StringExtractor(textoNuevo);

        //    IEnumerable<IWord> words = terms.CountOccurences().SortByOccurences().Cast<IWord>();

        //    foreach (var item in words) {

        //        Palabras.Add(item.Text);
        //        Frecuencias.Add(item.Occurrences);
        //    }






        // IBlacklist blacklist = CreateBlacklist(true);
        // IBlacklist customBlacklist = CommonBlacklist.CreateFromTextFile(s_BlacklistTxtFileName);
        // IEnumerable<string> terms =  CreateExtractor(textoNuevo);
        //   IEnumerable<string> terms = new List<string>();
        //  IEnumerable<IWord> words = terms.CountOccurences().SortByOccurences();

        //return words.Cast<string>();
        //}

        //public void GenerarNube(string texto)
        //{
        //    string textoNuevo = texto.RemoveStopWords("es");

        //List<string> palabras = new List<string>();

        //StringBuilder word = new StringBuilder();
        //foreach (char ch in texto)
        //{
        //    if (char.IsLetterOrDigit(ch))
        //    {
        //        word.Append(ch);
        //    }
        //    else
        //    {
        //        if (word.Length > 1)
        //        {
        //            palabras.Add(word.ToString());
        //        }
        //        word.Clear();
        //    }
        //}

    }
}

