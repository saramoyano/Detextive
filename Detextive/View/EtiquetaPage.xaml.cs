using AccesoDatos.Model;
using Detextive.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Detextive.View
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EtiquetaPage : Page
    {
        public EtiquetaViewModel etiquetaVM;
        public CitaViewModel citaVM;
        private Etiqueta etiqueta;
        private Cita cita;
        Proyecto proyecto;
        bool itemSeleccionado;
        public EtiquetaPage()
        {
            this.InitializeComponent();
           
           // citaVM = new CitaViewModel();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter.GetType().Equals(typeof(Proyecto)))
            {

                proyecto = (Proyecto)e.Parameter; 
                etiquetaVM = new EtiquetaViewModel(proyecto);
                itemSeleccionado = false; 
            }
        }

        private void lvEtiquetas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            etiqueta = (Etiqueta)lvEtiquetas.SelectedItem;
            itemSeleccionado = true;
        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(etiqueta != null)
            {
                etiquetaVM.EliminarEtiqueta(etiqueta);
            }
            
        }

        private void btAnalizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btNube_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvCitas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btDocPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.DocumentoPage), proyecto);

        }

        private void btEtiPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.EtiquetaPage), proyecto);

        }

        private void btNubePage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NubePages), proyecto);

        }

        private void btProyPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NubePages), proyecto);
        }

        private void Agregar(object sender, RoutedEventArgs e)
        {

        }

        private void btActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (etiqueta != null) { 
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textoIngresado = tbCambiarNombre.Text;
            if (etiqueta != null && textoIngresado != "") {
                etiqueta.Nombre = textoIngresado;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string textoIngresado = tbEtiqueta.Text;
            if (textoIngresado != "")
            {
                etiqueta = new Etiqueta();
                etiqueta.IdProy = proyecto.Id;
                etiqueta.Nombre = textoIngresado;
            }
        }
    }
}
