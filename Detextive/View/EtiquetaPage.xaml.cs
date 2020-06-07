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
    
    public sealed partial class EtiquetaPage : Page
    {
        public EtiquetaViewModel etiquetaVM;
        public CitaViewModel citaVM;
        private Etiqueta etiqueta;
        private Cita cita;
        List<Cita> citasE;
        Documento documento;
        DocumentoViewModel docVM;


        public EtiquetaPage()
        {
            this.InitializeComponent();
            citasE = new List<Cita>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (ProyectosPage.Proyecto != null)
            {
                etiquetaVM = new EtiquetaViewModel(ProyectosPage.Proyecto);    
                btAgregar.IsEnabled = true;           
            }
                btEliminar.IsEnabled = false;
                btActualizar.IsEnabled = false;               
                btQuitarEtiqueta.IsEnabled = false;
        }

        private void lvEtiquetas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                etiqueta = (Etiqueta)lvEtiquetas.SelectedItem;
                citaVM = new CitaViewModel(etiqueta);
                //etiqueta.Citas = (List<Cita>)citaVM.citas;
                etiqueta.NumCitas = citaVM.citas.Count;
                btEliminar.IsEnabled = true;
                btActualizar.IsEnabled = true;
                btAgregar.IsEnabled = true;
                btQuitarEtiqueta.IsEnabled = true;
                citasE = new List<Cita>();
                foreach (Cita cita in citaVM.citas)
                {
                    citasE.Add(cita);
                }
                lvCitas.ItemsSource = citasE;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No se puede ver la lista");
            }
        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvEtiquetas.SelectedItem != null)
                {
                    //foreach (Cita cita in citasE) {
                    //    citaVM.EliminarCita(cita);
                    //}   
                    Etiqueta et = (Etiqueta)lvEtiquetas.SelectedItem;
                    etiquetaVM.EliminarEtiqueta(etiqueta);
                    ProyectosPage.Proyecto.NumEtiquetas = ProyectosPage.Proyecto.NumEtiquetas - 1;
                    MostrarDialog(1);
                    this.Frame.Navigate(typeof(EtiquetaPage));

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No se puede eliminar la etiqueta");
            }
        }
        private void lvCitas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvCitas.SelectedItem != null)
            {
                btQuitarEtiqueta.IsEnabled = true;
                cita = (Cita)lvCitas.SelectedItem;
               

            }
        }

        private async void MostrarDialog(int op)
        {
            switch (op)
            {
                case 1:
                    ContentDialog proyDialog = new ContentDialog()
                    {
                        Title = "Etiqueta eliminada",
                        Content = "Se ha eliminado la etiqueta.",
                        PrimaryButtonText = "Entendido"
                    };
                    await proyDialog.ShowAsync();
                    break;
                case 2:
                    ContentDialog etiquetaAgregada = new ContentDialog
                    {
                        Title = "Etiqueta actualizada",
                        Content = "Se ha cambiado el nombre a la etiqueta ",
                        CloseButtonText = "Aceptar"
                    };
                    await etiquetaAgregada.ShowAsync();

                    break;
               
                case 3:
                    ContentDialog nombreCorto = new ContentDialog
                    {
                        Title = "Nombre no valido",
                        Content = "El nombre debe tener al menos 3 caracteres ",
                        CloseButtonText = "Aceptar"
                    };
                    await nombreCorto.ShowAsync();

                    break;
                case 4:
                    ContentDialog duplicada = new ContentDialog
                    {
                        Title = "Nombre no valido",
                        Content = "Ya existe una etiqueta con el mismo nombre ",
                        CloseButtonText = "Aceptar"
                    };
                    await duplicada.ShowAsync();

                    break;
            }
        }
        private void CambiarNombre(object sender, RoutedEventArgs e)
        {
            string textoIngresado = tbCambiarNombre.Text;
            try
            {
                if (etiqueta != null && textoIngresado != "" && textoIngresado.Length>3)
                {
                    etiqueta.Nombre = textoIngresado;
                    etiquetaVM.ActualizarEtiqueta(etiqueta);
                    flyCambiarNombre.Hide();
                    MostrarDialog(2);
                }
                else
                {
                    MostrarDialog(3);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
        private void AgregarAceptar(object sender, RoutedEventArgs e)
        {
            string textoIngresado = tbEtiqueta.Text;
            if (textoIngresado != "" && textoIngresado.Length > 3)
            {
                try
                {
                    etiqueta = new Etiqueta();
                    etiqueta.ProyectoId = ProyectosPage.Proyecto.Id;
                    etiqueta.Nombre = textoIngresado;
                    etiqueta.NumCitas = 0;
                    flyNvaEtiqueta.Hide();
                    bool existe = etiquetaVM.ExisteEtiqueta(etiqueta, ProyectosPage.Proyecto);
                    if (!existe)
                    {
                        etiquetaVM.AgregarEtiqueta(etiqueta);
                        // ProyectosPage.Proyecto.Etiquetas.Add(etiqueta);
                        this.Frame.Navigate(typeof(EtiquetaPage));
                    }
                    else
                    {
                        MostrarDialog(3);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }
        private void QuitarEtiqueta_Click(object sender, RoutedEventArgs e)
        {
            if (lvEtiquetas.SelectedItem != null && lvCitas.SelectedItem != null)
            {
                try
                {
                    
                   // etiqueta.Citas.Remove(cita);
                    citaVM.EliminarCita(cita);
                    etiqueta.NumCitas = etiqueta.NumCitas - 1;
                    this.Frame.Navigate(typeof(EtiquetaPage));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
//private void btAnalizar_Click(object sender, RoutedEventArgs e)
//{

//}

//private void btNube_Click(object sender, RoutedEventArgs e)
//{

//}

//private void btAgregar_Click(object sender, RoutedEventArgs e)
//{

//}
//private void btDocPage_Click(object sender, RoutedEventArgs e)
//{
//    this.Frame.Navigate(typeof(Detextive.View.DocumentoPage));

//}

//private void btEtiPage_Click(object sender, RoutedEventArgs e)
//{
//    this.Frame.Navigate(typeof(Detextive.View.EtiquetaPage));

//}

//private void btNubePage_Click(object sender, RoutedEventArgs e)
//{
//    this.Frame.Navigate(typeof(NubePages));

//}

//private void btProyPage_Click(object sender, RoutedEventArgs e)
//{
//    this.Frame.Navigate(typeof(NubePages));
//}

//private void Agregar(object sender, RoutedEventArgs e)
//{

//}

//private void btActualizar_Click(object sender, RoutedEventArgs e)
//{
//    if (etiqueta != null) { 

//    }
//}
