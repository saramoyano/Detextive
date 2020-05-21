using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Logica
{
    class LogicaDocumento
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


        public List<Model.Documento> ListaDocumentos()
        {
            using (var db = new Model.Context())
            {

                try
                {
                    return db.DocumentoSet.OrderBy(b => b.Id).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("No hay documentos guardados.", e);
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
                    throw new Exception("No se ha podido agregar el documento ", e);
                }
            }

        }

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
}
