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
    public sealed partial class NubePages : Page
    {
        public NubeViewModel nubeVM;
        Proyecto proyecto;
        Nube nube;
        Palabra palabra;

        public NubePages()
        {
            this.InitializeComponent();
            nubeVM = new NubeViewModel();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter.GetType().Equals(typeof(Proyecto)))
            {

                proyecto = (Proyecto)e.Parameter;
            }
        }


        private void btEtiPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.EtiquetaPage), proyecto);
        }

        private void btNubePage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.NubePages), proyecto);
        }

        private void btProyPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProyectoPage), proyecto);
        }

        private void lvNubes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lvPalabras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDocPage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
