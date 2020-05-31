using AccesoDatos.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.ViewModel
{
    public class PalabraViewModel
    {
        public ObservableCollection<Palabra> palabras;

        public PalabraViewModel()
        {
            palabras =  AccesoDatos.Logica.LogicaPalabra.GetInstance().ListaPalabras();
        }

        public bool ExistePalabra(Palabra pal, Nube n, Proyecto p)
        {
            return AccesoDatos.Logica.LogicaPalabra.GetInstance().ExistePalabra(pal, p);
        }
            //public ObservableCollection<Palabra> ListaPalabras()
            //{
            //    palabras = 
            //    return palabras;
            //}

            //public ObservableCollection<Palabra> ListaPalabrasFiltro(Palabra palabra)
            //{
            //    palabras = AccesoDatos.Logica.LogicaPalabra.GetInstance().ListaPalabrasFiltro(palabra);
            //    return palabras;
            //}

            public void AgregarPalabra(Palabra palabra)
        {
            try
            {
                AccesoDatos.Logica.LogicaPalabra.GetInstance().AgregarPalabra(palabra);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public void ActualizarPalabra(Palabra palabra)
        {
            try
            {
                AccesoDatos.Logica.LogicaPalabra.GetInstance().ActualizarPalabra(palabra);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }
        public void EliminarPalabra(Palabra palabra)
        {
            try
            {
                AccesoDatos.Logica.LogicaPalabra.GetInstance().EliminarPalabra(palabra);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }

    }
}
