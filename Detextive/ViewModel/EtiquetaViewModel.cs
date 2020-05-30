using AccesoDatos.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.ViewModel
{
    public class EtiquetaViewModel 
    {
        public ObservableCollection<Etiqueta> etiquetas;

        public EtiquetaViewModel(Proyecto proyecto)
        {
            etiquetas =  AccesoDatos.Logica.LogicaEtiqueta.GetInstance().ListaEtiquetasFiltro(proyecto);
        }


        //public ObservableCollection<Etiqueta> ListaEtiquetas()
        //{
        //    etiquetas =
        //    return etiquetas;
        //}

        //public ObservableCollection<Etiqueta> ListaEtiquetasFiltro(Etiqueta etiqueta)
        //{
        //    etiquetas = AccesoDatos.Logica.LogicaEtiqueta.GetInstance().
        //    return etiquetas;
        //}

        public void AgregarEtiqueta(Etiqueta etiqueta)
        {
            try
            {
                AccesoDatos.Logica.LogicaEtiqueta.GetInstance().AgregarEtiqueta(etiqueta);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public bool ExisteEtiqueta(Etiqueta etiqueta) { return false; }

        public void ActualizarEtiqueta(Etiqueta etiqueta)
        {
            try
            {
                AccesoDatos.Logica.LogicaEtiqueta.GetInstance().ActualizarEtiqueta(etiqueta);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }
        public void EliminarEtiqueta(Etiqueta etiqueta)
        {
            try
            {
                AccesoDatos.Logica.LogicaEtiqueta.GetInstance().EliminarEtiqueta(etiqueta);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }

    }
}
