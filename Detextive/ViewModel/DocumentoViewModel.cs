using AccesoDatos.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.ViewModel
{
    public class DocumentoViewModel
    {

        public ObservableCollection<Documento> documentos;

        public DocumentoViewModel()
        {
            documentos = AccesoDatos.Logica.LogicaDocumento.GetInstance().ListaDocumentos();
        }


        //public ObservableCollection<Documento> ListaDocumentos()
        //{
        //    documentos = 
        //    return documentos;
        //}

        public Documento ListaDocumentosFiltro(Documento doc)
        {             
            return AccesoDatos.Logica.LogicaDocumento.GetInstance().ListaDocumentosFiltro(doc);
        }
        public void AgregarDocumento(Documento documento)
        {
            try
            {
                AccesoDatos.Logica.LogicaDocumento.GetInstance().AgregarDocumento(documento);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public void ActualizarDocumento(Documento documento)
        {
            try
            {
                AccesoDatos.Logica.LogicaDocumento.GetInstance().ActualizarDocumento(documento);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }
        public void EliminarDocumento(Documento documento)
        {
            try
            {
                AccesoDatos.Logica.LogicaDocumento.GetInstance().EliminarDocumento(documento);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }

    }
}
