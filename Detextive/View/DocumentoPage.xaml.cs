using AccesoDatos.Model;
using Detextive.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public DocumentoViewModel documentoVM;
        public CitaViewModel citaVM;
        public NubeViewModel nubeVM; 
        Documento documento;
        Nube nube; 
        Cita cita;
        List<Cita> citasD;

        public DocumentoPage()
        {
            this.InitializeComponent();
            citasD = new List<Cita>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (ProyectosPage.Proyecto != null)
            {
                documentoVM = new DocumentoViewModel(ProyectosPage.Proyecto);
                btAgregar.IsEnabled = true;
            }
            else {
                btAgregar.IsEnabled = false;
            }
            btEliminar.IsEnabled = false;            
                 
        }
        private void lvDocs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btEliminar.IsEnabled = true;                
                btAgregar.IsEnabled = true;   
                documento = (Documento)lvDocs.SelectedItem;
                citaVM = new CitaViewModel(documento); 
                citasD = new List<Cita>();
                foreach (Cita cita in citaVM.citas)
                {
                    citasD.Add(cita);
                }
                lvCitas.ItemsSource = citasD;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void Eliminar_Documento(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvDocs.SelectedItem != null && (lvDocs.SelectedItems.Count == 1))
                {

                    foreach (Cita cita in citasD)
                    {
                        citaVM.EliminarCita(cita);

                    }
                    documento = (Documento)lvDocs.SelectedItem;                   
                    documentoVM.EliminarDocumento(documento);
                    ProyectosPage.Proyecto.NumDocumentos = ProyectosPage.Proyecto.NumDocumentos - 1;
                    ProyectosPage.Proyecto.NombreDocActivo = "";
                    ProyectosPage.Proyecto.Token = "";

                    MostrarDialog(1);
                }
                this.Frame.Navigate(typeof(DocumentoPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private async void MostrarDialog(int op)
        {
            switch (op)
            {
                case 1:
                    ContentDialog proyDialog = new ContentDialog()
                    {
                        Title = "Documento eliminado",
                        Content = "Se ha eliminado el documento.",
                        PrimaryButtonText = "Entendido"
                    };
                    await proyDialog.ShowAsync();
                    break;
                case 2:
                    ContentDialog etiquetaAgregada = new ContentDialog
                    {
                        Title = "Cita agregada",
                        Content = "",
                        CloseButtonText = "Aceptar"
                    };
                    await etiquetaAgregada.ShowAsync();

                    break;
            }
        }
                    private void AgregarDocumento(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProyectosPage));
        }          
         
    }
}
