using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
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

        private ObservableCollection<AccesoDatos.Proyecto> proyectos;

        public ProyectoViewModel() {
            proyectos = new ObservableCollection<AccesoDatos.Proyecto>();

            
        }


        public ObservableCollection<AccesoDatos.Proyecto> ListaProyectos() { 
            proyectos = AccesoDatos.Logica.LogicaProyecto.GetInstance().ListaProyectos();
            return proyectos;
        }
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
