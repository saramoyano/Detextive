using AccesoDatos.Logica;
using AccesoDatos.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input.Inking.Analysis;

namespace Detextive.ViewModel
{
    public class NubeViewModel
    {
        public ObservableCollection<Nube> nubes;

        public NubeViewModel(Proyecto p)
        {
            nubes =  AccesoDatos.Logica.LogicaNube.GetInstance().ListaNubeFiltro(p);
        }

        public bool ExisteNube(Documento d, Proyecto p)
        {
            return AccesoDatos.Logica.LogicaNube.GetInstance().ExisteNube(d, p);
        }
        public void AgregarNube(Nube nube)
        {
            try
            {
                AccesoDatos.Logica.LogicaNube.GetInstance().AgregarNube(nube);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public Nube GetNubeFiltro(Documento d, Proyecto p) {
            return AccesoDatos.Logica.LogicaNube.GetInstance().NubeFiltro(d,p);
        }

        public void ActualizarNube(Nube nube)
        {
            try
            {
                AccesoDatos.Logica.LogicaNube.GetInstance().ActualizarNube(nube);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }
        public void EliminarNube(Nube nube)
        {
            try
            {
                AccesoDatos.Logica.LogicaNube.GetInstance().EliminarNube(nube);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }

    }
}
