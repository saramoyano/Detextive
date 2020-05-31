using AccesoDatos.Model;
using Detextive.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class DocumentoPage : Page
    {
        Proyecto proyecto;
        public DocumentoViewModel documentoVM;
        public CitaViewModel citaVM;
        Documento documento;
        public DocumentoPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter.GetType().Equals(typeof(Proyecto)))
            {

                proyecto = (Proyecto)e.Parameter;
                documentoVM = new DocumentoViewModel(proyecto);
                
            }
        }

        private void lvDocs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            documento = (Documento)lvDocs.SelectedItem;
            citaVM = new CitaViewModel(documento);

            proyecto.NombreDocActivo = documento.Nombre;

            //ScrollViewer scrollViewer = lvDocs.Parent as ScrollViewer;
            //if (scrollViewer != null)
            //{
            //    var container = lvDocs.ContainerFromItem(lvDocs.SelectedItem);
            //    var item = container as ListViewItem;
            //    var objTransf = item.TransformToVisual(lvDocs);
            //    Point point = objTransf.TransformPoint(new Point(0, 0));

            //    scrollViewer.ChangeView(point.X, 0, 1);
            //}
        }

        private void Eliminar_Documento(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvDocs.SelectedItem != null && (lvDocs.SelectedItems.Count ==1))
                {
                    string docEliminado = documento.Nombre;
                    documentoVM.EliminarDocumento(documento);

                    ContentDialog documentoEliminado = new ContentDialog
                    {
                        Title = "Documento eliminado",
                        Content = "Se ha ha eliminado el documento" + docEliminado,
                        CloseButtonText = "Aceptar"
                    };
                }
            }
            catch (Exception ex) {
                
            }
        }

        private void Analizar(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProyectoPage), proyecto);
        }

        private void NubeMultidocumento(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NubePages), proyecto);          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProyectoPage), proyecto);
        }

        private void btEtiPage_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.EtiquetaPage), proyecto);
        }

        private void btDocPage_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.DocumentoPage), proyecto);
        }

        private void btNubePage_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.NubePages), proyecto);
        }

        private void btProyPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProyectoPage), proyecto);
        }
    }
}
