using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.ViewModel
{
    class DocumentoViewModel
    {

        private ObservableCollection<AccesoDatos.Documento> documentos;

        public DocumentoViewModel()
        {
            documentos = new ObservableCollection<AccesoDatos.Documento>();


        }


        public ObservableCollection<AccesoDatos.Documento> ListaDocumentos()
        {
            documentos = AccesoDatos.Logica.LogicaDocumento.GetInstance().ListaDocumentos();
            return documentos;
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
