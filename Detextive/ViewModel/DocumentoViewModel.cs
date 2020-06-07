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

        public DocumentoViewModel(Proyecto proyecto)
        {
            documentos = AccesoDatos.Logica.LogicaDocumento.GetInstance().ListaDocumentosFiltro(proyecto);
        }

        public bool ExisteDocumento(string nombre, Proyecto p) {
            return AccesoDatos.Logica.LogicaDocumento.GetInstance().ExisteDocumento(nombre,p);
        }
        public Documento GetDocumento(string nombre, Proyecto p)
        {           
                return AccesoDatos.Logica.LogicaDocumento.GetInstance().GetDocumento(nombre, p );
        }
        public Documento GetDocumentoUbicacion(string ubicacion)
        {
            return AccesoDatos.Logica.LogicaDocumento.GetInstance().GetDocumentoUbicacion(ubicacion);
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
