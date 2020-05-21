using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.ViewModel
{
    class NubePageViewModel
    {
        private ObservableCollection<AccesoDatos.Nube> nubes;

        public NubeViewModel()
        {
            nubes = new ObservableCollection<AccesoDatos.Nube>();


        }


        public ObservableCollection<AccesoDatos.Nube> ListaNubes()
        {
            nubes = AccesoDatos.Logica.LogicaNube.GetInstance().ListaNubes();
            return nubes;
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
