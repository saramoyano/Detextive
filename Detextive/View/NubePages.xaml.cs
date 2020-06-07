using AccesoDatos.Model;
using Detextive.Auxiliar;
using Detextive.ViewModel;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using Windows.ApplicationModel.Contacts;
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
    public sealed partial class NubePages : Page
    {
        public NubeViewModel nubeVM;
        DocumentoViewModel documentoVM;
        ProyectoViewModel proyectoVM;
        Documento documento;
        Nube nube;

        PalabraViewModel palabraVM;
        List<Palabra> palabrasN;
        CrearNube cn;
        string text;
        IReadOnlyList<StorageFile> files;
        public NubePages()
        {
            cn = new CrearNube();
            this.InitializeComponent();
            palabrasN = new List<Palabra>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {

                if (ProyectosPage.Proyecto != null)
                {
                    files = new List<StorageFile>();
                    proyectoVM = new ProyectoViewModel();
                    nubeVM = new NubeViewModel(ProyectosPage.Proyecto);
                    documentoVM = new DocumentoViewModel(ProyectosPage.Proyecto);
                    palabraVM = new PalabraViewModel(ProyectosPage.Proyecto);
                    palabrasN = new List<Palabra>();
                    Crear.IsEnabled = true;
                    foreach (Palabra palabra in palabraVM.palabras)
                    {
                        palabrasN.Add(palabra);
                    }
                    lvPalabras.ItemsSource = palabrasN;
                    if (e.Parameter != null)
                    {
                        text = (string)e.Parameter;
                        CloudControl.WeightedWords = cn.GenerarNube(text);

                        if (ProyectosPage.Proyecto.NombreDocActivo != null && ProyectosPage.Proyecto.NombreDocActivo != "")
                        {
                            documento = documentoVM.GetDocumento(ProyectosPage.Proyecto.NombreDocActivo, ProyectosPage.Proyecto);
                            if (!nubeVM.ExisteNube(documento, ProyectosPage.Proyecto))
                            {
                                GuardarNube();
                            }
                            else
                            {
                                nube = nubeVM.GetNubeFiltro(documento, ProyectosPage.Proyecto);
                            }
                        }
                    }
                    else
                    {
                        MostrarDialog(2);
                    }

                   
                }
                else 
                {
                     btEliminar.IsEnabled = false;
                    Crear.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ex" + ex.StackTrace);
            }
        }

        public void GuardarNube()
        {
            try
            {
                nube = new Nube();
                if (files.Count > 1)
                {
                    nube.NumDocumentos = files.Count;
                }
                else
                {
                    nube.NumDocumentos = 1;
                    nube.DocumentoId = documento.Id;
                }

                nube.ExtensionFragmento = cn.Extension;
                documento.Extension = cn.Extension;
                nube.ProyectoId = ProyectosPage.Proyecto.Id;
                nube.NumConceptos = CloudControl.WeightedWords.Count();

                nubeVM.AgregarNube(nube);
                Palabra palabra;
                if (nube.NumDocumentos == 1)
                {
                    documento.NubeId = nube.Id;
                    foreach (var item in CloudControl.WeightedWords)
                    {
                        palabra = new Palabra();
                        palabra.ProyectoId = ProyectosPage.Proyecto.Id;
                        palabra.NubeId = nube.Id;
                        palabra.Nombre = item.Text;
                        palabra.NumApariciones = item.Occurrences;
                        palabraVM.AgregarPalabra(palabra);
                        nube.Palabras.Add(palabra);
                        palabrasN.Add(palabra);
                    }
                }
                else
                {
                    foreach (var item in CloudControl.WeightedWords)
                    {
                        palabra = new Palabra();
                        palabra.ProyectoId = ProyectosPage.Proyecto.Id;
                        palabra.NubeId = nube.Id;
                        palabra.Nombre = "multidoc: " + item.Text;
                        palabra.NumApariciones = item.Occurrences;
                        palabraVM.AgregarPalabra(palabra);
                        nube.Palabras.Add(palabra);
                        palabrasN.Add(palabra);
                    }
                }
                lvPalabras.ItemsSource = palabrasN;
                nubeVM.ActualizarNube(nube);
                ProyectosPage.Proyecto.Nubes.Add(nube);
                proyectoVM.ActualizarProyecto(ProyectosPage.Proyecto);
                documentoVM.ActualizarDocumento(documento);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void lvDocumentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvDocumentos.SelectedItem != null) //
            {
                btEliminar.IsEnabled = true;

                try
                {
                    documento = (Documento)lvDocumentos.SelectedItem;
                    nube = nubeVM.GetNubeFiltro(documento, ProyectosPage.Proyecto);
                 
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }

        private void lvPalabras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            //esto solo sirve por si el usuario quiere modificar el documento y hacer la nube de nuevo
            try
            {
                if (lvDocumentos.SelectedItem != null && lvDocumentos.SelectedItems.Count == 1)
                {

                    nube = nubeVM.GetNubeFiltro((Documento)lvDocumentos.SelectedItem, ProyectosPage.Proyecto);
                    if (nube != null)
                    {
                        nubeVM.EliminarNube(nube);

                    }
                    MostrarDialog(1);

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("No se puede eliminar la etiqueta");
            }
        }

        private async void MostrarDialog(int op)
        {
            switch (op)
            {
                case 1:
                    ContentDialog proyDialog = new ContentDialog()
                    {
                        Title = "Confirmación",
                        Content = "Se ha eliminado la nube. El documento permanece guardado. Para borrarlo, vaya a la pagina Documentos",
                        PrimaryButtonText = "Entendido"
                    };
                    await proyDialog.ShowAsync();
                    break;
                case 2:
                    ContentDialog noHaynube = new ContentDialog
                    {
                        Title = "No se muestran nubes",
                        Content = "Para ver una nube, vaya a la página Proyecto, abra un documento en el editor y haga click en Ver la Nube",
                        CloseButtonText = "Entendido"
                    };
                    await noHaynube.ShowAsync();

                    break;
            }
        }

        private async void Crear_Click(object sender, RoutedEventArgs e)
        {
            string textoNubeMultidoc = "";
            try
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add(".rtf");

                files = await picker.PickMultipleFilesAsync();
                if (files != null && files.Count > 1)
                {
                    foreach (StorageFile file in files)
                    {
                        if (file != null)
                        {
                            var text = await FileIO.ReadTextAsync(file, Windows.Storage.Streams.UnicodeEncoding.Utf8);
                            textoNubeMultidoc = textoNubeMultidoc + text;
                        }
                    }
                    CloudControl.WeightedWords = cn.GenerarNube(textoNubeMultidoc);
                    GuardarNube();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
