using AccesoDatos.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.ViewModel
{
    class CitaViewModel
    {
        private List<Cita> citas;

        public CitaViewModel()
        {
            citas = new List<Cita>();

        }


        public List<Cita> ListaCitas()
        {
            citas = AccesoDatos.Logica.LogicaCita.GetInstance().ListaCitas();
            return citas;
        }
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
