using AccesoDatos.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Detextive.ViewModel
{
    public class ProyectoViewModel
    {

        public ObservableCollection<Proyecto> proyectos;

        public ProyectoViewModel() {
            proyectos =  AccesoDatos.Logica.LogicaProyecto.GetInstance().ListaProyectos();

            
        }


        //public List<Proyecto> ListaProyectos() { 
        //    proyectos = 
        //    return proyectos;
        //}
        public void AgregarProyecto(Proyecto proyecto) {
            try
            {
                AccesoDatos.Logica.LogicaProyecto.GetInstance().AgregarProyecto(proyecto);
            }
            catch (Exception e) {
                throw new Exception("Error",e);               
            }
        }

        public void ActualizarProyecto(Proyecto proyecto) {
            try
            {
                AccesoDatos.Logica.LogicaProyecto.GetInstance().ActualizarProyecto(proyecto);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
            
        }
        public void EliminarProyecto(Proyecto proyecto) {
            try
            {
                AccesoDatos.Logica.LogicaProyecto.GetInstance().EliminarProyecto(proyecto);
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

        }
       

    }
}
