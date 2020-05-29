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
    class NubeViewModel
    {
        private List<Nube> nubes;

        public NubeViewModel()
        {
            nubes = new List<Nube>();
        }


        //public List<string> ObtenerPalabras( )
        //{
        //    return LogicaNube.GetInstance().Palabras;
                
        //}

        //public List<int> ObtenerFrecuencias( )
        //{
        //    return LogicaNube.GetInstance().Frecuencias;

        //}

        //public void ProcesarTexto(string texto)
        //{
        //    LogicaNube.GetInstance().ProcessText(texto);
        //}


        public List<Nube> ListaNubes()
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
