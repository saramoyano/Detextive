using AccesoDatos.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.ViewModel
{
   public class CitaViewModel
    {
        public ObservableCollection<Cita> citas;

        public CitaViewModel(Documento d)
        {
            citas =  AccesoDatos.Logica.LogicaCita.GetInstance().ListaCitasFiltro(d);
        }
        public CitaViewModel(Etiqueta etiqueta)
        {
            citas = AccesoDatos.Logica.LogicaCita.GetInstance().ListaCitasFiltro(etiqueta);
        }

        //public ObservableCollection<Cita> ListaCitas()
        //{
        //    citas = 
        //    return citas;
        //}


        //public ObservableCollection<Cita> ListaCitasFiltro(Etiqueta etiqueta)
        //{
        //    citas = AccesoDatos.Logica.LogicaCita.GetInstance().ListaCitasFiltro(etiqueta);
        //    return citas;
        //}
        public void AgregarCita(Cita cita)
        {
            try
            {
                AccesoDatos.Logica.LogicaCita.GetInstance().AgregarCita(cita);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public void ActualizarCita(Cita cita)
        {
            try
            {
                AccesoDatos.Logica.LogicaCita.GetInstance().ActualizarCita(cita);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }
        public void EliminarCita(Cita cita)
        {
            try
            {
                AccesoDatos.Logica.LogicaCita.GetInstance().EliminarCita(cita);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }


    }
}
