using AccesoDatos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AccesoDatos.Logica
{
    public class LogicaDocumento
    {
        private static LogicaDocumento logicaDocumento;

        public static LogicaDocumento GetInstance()
        {
            if (logicaDocumento == null)
            {
                logicaDocumento = new LogicaDocumento();
            }
            return logicaDocumento;
        }

        // Obtener la lista de Documentos
        // params: ninguno
        public ObservableCollection<Model.Documento> ListaDocumentos()
        {
            ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();

            using (var db = new Model.Context())
            {

                try
                {
                    List<Documento> docs = db.DocumentoSet.OrderBy(b => b.Id).ToList();
                    foreach (Documento documento in docs)
                    {
                        documentos.Add(documento);
                    }
                    return documentos;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay documentos guardados.", e);
                }

            }

        }

        // Obtener una  lista de Documentos según filtro aplicado (propiedad idProyecto)
        // params: objeto de la clase AccesoDatos.Model.Documento
        public ObservableCollection<Model.Documento> ListaDocumentosFiltro(Proyecto proyecto)
        {

            using (var db = new Model.Context())
            {
                ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();
                try
                {
                    var lDocs = db.DocumentoSet.Where(b => b.ProyectoId.Equals(proyecto.Id)).ToList();

                    foreach (Documento documento in lDocs)
                    {
                        documentos.Add(documento);
                    }
                    return documentos;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay documentos guardados.", e);
                }

            }

        }

        public Documento GetDocumento(string nombre)
        {
            using (var db = new Model.Context())
            {
                return db.DocumentoSet.Single(b => b.Nombre.Equals(nombre));

            }               
        }

        //Determina si el documento existe
        //params Objeto tipo documento(propiedad Nombre)
        public bool ExisteDocumento(string nombre, Proyecto p)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    List<Documento> lDocs = new List<Documento>();
                    lDocs = db.DocumentoSet.Where(b => b.ProyectoId.Equals(p.Id) && b.Nombre.Equals(nombre)).ToList();
                    if (lDocs.Count > 0)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (Exception e)
                {                  
                    throw new Exception("Hubo un error en base de datos.", e);
                }
            }
        }

        // Agregar un documento a la lista 
        // params: objeto de la clase AccesoDatos.Model.Documento
        public void AgregarDocumento(Documento documento)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    db.DocumentoSet.Add(documento);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido agregar el documento", e);
                }
            }

        }


        // Actualiza un documento de la lista 
        // params: objeto de la clase AccesoDatos.Model.Documento con los datos actualizados
        public void ActualizarDocumento(Documento documento)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var doc = db.DocumentoSet.FirstOrDefault(x => x.Id == documento.Id);
                    db.Entry(doc).State = EntityState.Modified;
                    db.Entry(doc).CurrentValues.SetValues(documento);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido actualizar el documento ", e);

                }
            }
        }

        // Elimina un documento de la lista 
        // params: objeto de la clase AccesoDatos.Model.Documento
        public void EliminarDocumento(Documento documento)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var doc = db.DocumentoSet.FirstOrDefault(x => x.Id == documento.Id);
                    db.DocumentoSet.Remove(doc);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido eliminar el documento ", e);

                }
            }
        }
    }
}
