using AccesoDatos.Model;
using Detextive.ViewModel;
using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Gma.CodeCloud.Controls.TextAnalyses.Extractors;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist;
using System.Diagnostics;
using Windows.UI.Xaml.Navigation;
using System.Linq;
using WordCloudControl;
using Windows.Storage;
using System.Linq.Expressions;
using Windows.UI.Xaml.Media.Animation;
using Detextive.View;
using System.Drawing;
using Windows.UI.Xaml.Media;
using System.IO;
using System.Collections;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;


namespace Detextive
{
    public sealed partial class ProyectosPage : Page
    {
        private ProyectoViewModel proyectoVM;
        private EtiquetaViewModel etiqVM;
        private DocumentoViewModel documentoVM;
        private NubeViewModel nubeVM;
        private CitaViewModel citaVM;
        private PalabraViewModel palabraVM;
        private static Proyecto proyecto;
        private Etiqueta etiqueta;
        private Documento documento;
        private Cita cita;
        private Nube nube;
        private Palabra palabra;

        private IBlacklist _blacklist;
        private IProgressIndicator _progress;
        string texto;

        StorageFile file;

        public static Proyecto Proyecto { get => proyecto; set => proyecto = value; }

        public ProyectosPage()
        {
            this.InitializeComponent();
            proyectoVM = new ProyectoViewModel();
            _blacklist = new BannedWords();         
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (proyecto != null)
            {

                openFileButton.IsEnabled = true;
                saveFileButton.IsEnabled = true;
                underlineButton.IsEnabled = true;
                btnEtiqueta.IsEnabled = true;
                btnNube.IsEnabled = false;
                btCambiarNombre.IsEnabled = true;
                btnCerrarProy.IsEnabled = true;
                btnProyecto.IsEnabled = false;
                btnAbrirProy.IsEnabled = false;
                btEliminar.IsEnabled = false;
                etiqVM = new EtiquetaViewModel(Proyecto);
                documentoVM = new DocumentoViewModel(Proyecto);
                nubeVM = new NubeViewModel(Proyecto);
                if (Proyecto.Token != null && Proyecto.Token != "")
                {
                    AbrirReciente();
                }
            }

            if (Proyecto == null)
            {
                openFileButton.IsEnabled = false;
                saveFileButton.IsEnabled = false;
                underlineButton.IsEnabled = false;
                btCambiarNombre.IsEnabled = false;
                btnEtiqueta.IsEnabled = false;
                btnNube.IsEnabled = false;
                btnCerrarProy.IsEnabled = false;
                MostrarDialog(1);
            }

        }
        private void AceptaNombreProyecto(object sender, RoutedEventArgs e)
        {
            string textoIngresado = textBoxProy.Text;
            if (textoIngresado != "" && textoIngresado.Length > 3)
            {
                try
                {
                    Proyecto = new Proyecto();
                    Proyecto.Nombre = textoIngresado;
                    Proyecto.NumDocumentos = 0;
                    Proyecto.NumEtiquetas = 0;
                    if (!proyectoVM.ExisteProyecto(Proyecto))
                    {
                        proyectoVM.AgregarProyecto(Proyecto);
                        Proyecto_Ok();
                        flyNombre.Hide();
                    }
                    else
                    {
                        MostrarDialog(10);

                    }
                }
                catch (Exception excepcion)
                {
                    ContentDialog error = new ContentDialog
                    {
                        Title = "Hubo un problema",
                        Content = excepcion.Message,
                        CloseButtonText = "Aceptar"
                    };
                }
            }
            else
            {
                MostrarDialog(11);
            }
        }
        private void Proyecto_Ok()
        {
            proyecto.Documentos = new List<Documento>();
            proyecto.Etiquetas = new List<Etiqueta>();
            proyecto.Nubes = new List<Nube>();

            this.Frame.Navigate(typeof(ProyectosPage));
        }
        private void lvProyectos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Proyecto = new Proyecto();
            Proyecto = (Proyecto)lvProyectos.SelectedItem;
            flyListaProy.Hide();
            Proyecto_Ok();
        }
        private void AceptarEtiqueta(object sender, RoutedEventArgs e)
        {
            string textoIngresado = tbEtiqueta.Text;
            if (textoIngresado != "" && textoIngresado.Length > 3)
            {
                try
                {
                    etiqueta = new Etiqueta();
                    etiqueta.ProyectoId = Proyecto.Id;
                    Proyecto.NumEtiquetas = Proyecto.NumEtiquetas + 1;
                    etiqueta.Nombre = textoIngresado;
                    etiqueta.NumCitas = 0;
                    flyNvaEtiqueta.Hide();
                    bool existe = etiqVM.ExisteEtiqueta(etiqueta, proyecto);
                    if (!existe)
                    {
                        etiqVM.AgregarEtiqueta(etiqueta);
                        Proyecto.Etiquetas.Add(etiqueta);
                        proyectoVM.ActualizarProyecto(Proyecto);
                       // MostrarDialog(6);
                        this.Frame.Navigate(typeof(ProyectosPage));
                    }
                    else
                    {
                        MostrarDialog(7);
                    }
                }
                catch (Exception excepcion)
                {
                    Debug.Write(excepcion.StackTrace);
                }
            }
            else
            {
                MostrarDialog(11);
            }
        }
        private void CrearNubes(object sender, RoutedEventArgs e)
        {
            if (texto != "")
            {
                this.Frame.Navigate(typeof(Detextive.View.NubePages), texto);
            }
            else
            {
                this.Frame.Navigate(typeof(Detextive.View.NubePages));
            }

        }
        public async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".rtf");

            file = await picker.PickSingleFileAsync();


            if (file != null)
            {
                RecentStorageItemVisibility visibility = RecentStorageItemVisibility.AppAndSystem;
                string mruToken = StorageApplicationPermissions.MostRecentlyUsedList.Add(file, file.Name, visibility);
                Proyecto.Token = mruToken;

            }

            await CargarTexto();
            this.Frame.Navigate(typeof(ProyectosPage));
        }
        private async Task CargarTexto()
        {
            if (file != null)
            {
                if (file.FileType == ".rtf")
                {
                    try
                    {
                        Windows.Storage.Streams.IRandomAccessStream randAccStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                       
                        editor.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
                        editor.Document.GetText(TextGetOptions.None, out texto);
                        btnNube.IsEnabled = true;
                    }
                    catch (Exception excepcion)
                    {
                        //MostrarDialog(9);
                    }
                }
                string[] nombre = file.Name.Split(".");
                string name = nombre[0];
                Proyecto.NombreDocActivo = name;

                try
                {
                    if (!documentoVM.ExisteDocumento(name, Proyecto))
                    {
                        documento = new Documento();
                        documento.Ubicacion = file.Path;
                        documento.ProyectoId = Proyecto.Id;
                        Proyecto.NumDocumentos = Proyecto.NumDocumentos + 1;
                        documento.Nombre = name;
                        documentoVM.AgregarDocumento(documento);
                        proyecto.Documentos.Add(documento);
                        proyectoVM.ActualizarProyecto(Proyecto);
                    }
                    else
                    {
                        documento = documentoVM.GetDocumento(name, Proyecto);
                    }
                }
                catch (Exception excepcion)
                {
                   
                    Debug.Write(excepcion.StackTrace);
                }
            }
        }
        private async void AbrirReciente()
        {

           if (StorageApplicationPermissions.MostRecentlyUsedList.ContainsItem(Proyecto.Token))
            {

                file = await StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(Proyecto.Token);
                Debug.WriteLine(file.Name);
            }
            else
            {
                Debug.WriteLine("Error");
            }

            if (file != null)
            {
                await CargarTexto();

            }
        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.Pickers.FileSavePicker savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we
                // finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                Windows.Storage.Streams.IRandomAccessStream randAccStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);

                editor.Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, randAccStream);

                // Let Windows know that we're finished changing the file so the
                // other app can update the remote version of the file.
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status != Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    Windows.UI.Popups.MessageDialog errorBox =
                        new Windows.UI.Popups.MessageDialog("File " + file.Name + " couldn't be saved.");
                    await errorBox.ShowAsync();
                }
            }
        }
        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        { }
        private async void MostrarDialog(int op)
        {
            switch (op)
            {
                case 1:
                    ContentDialog proyDialog = new ContentDialog()
                    {
                        Title = "Comenzar",
                        Content = "Para comenzar a trabajar, cree un nuevo proyecto o abra uno existente.",
                        PrimaryButtonText = "Entendido"
                    };
                    await proyDialog.ShowAsync();
                    break;
                case 2:
                    ContentDialog etiquetaAgregada = new ContentDialog
                    {
                        Title = "Cita agregada",
                        Content = "Se ha agregado la cita a la etiqueta " + etiqueta.Nombre + ".",
                        CloseButtonText = "Aceptar"
                    };
                    await etiquetaAgregada.ShowAsync();

                    break;
                case 3:
                    ContentDialog error = new ContentDialog
                    {
                        Title = "Error",
                        Content = "No se ha podido agregar la cita",
                        CloseButtonText = "Aceptar"
                    };
                    await error.ShowAsync();
                    break;
                case 4:
                    ContentDialog etiquetaAsociada = new ContentDialog
                    {
                        Title = "Cita agregada",
                        Content = "Se ha agregado la cita a la etiqueta " + etiqueta.Nombre + ".",
                        CloseButtonText = "Aceptar"
                    };
                    await etiquetaAsociada.ShowAsync();

                    break;
                case 5:
                    ContentDialog eAError = new ContentDialog
                    {
                        Title = "Error",
                        Content = "No se ha podido agregar la cita",
                        CloseButtonText = "Aceptar"
                    };
                    await eAError.ShowAsync();

                    break;
                case 6:
                    ContentDialog okEti = new ContentDialog
                    {
                        Title = "Etiqueta agregada",
                        Content = "Se ha agregado la etiqueta",
                        CloseButtonText = "Aceptar"
                    };
                    await okEti.ShowAsync();
                    break;
                case 7:
                    ContentDialog errorNombre = new ContentDialog
                    {
                        Title = "Ya existe la etiqueta",
                        Content = "Ingrese un nombre distinto para la etiqueta",
                        CloseButtonText = "Aceptar"
                    };
                    await errorNombre.ShowAsync();
                    break;
                case 8:
                    ContentDialog errorDialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "No se ha podido agregar el documento",
                        CloseButtonText = "Ok"
                    };

                    await errorDialog.ShowAsync();
                    break;
                case 9:
                    ContentDialog errorFile = new ContentDialog()
                    {
                        Title = "Error al abrir el archivo",
                        Content = "No se pudo abrir el archivo. Compruebe que sea un documento rtf",
                        PrimaryButtonText = "Ok"
                    };

                    await errorFile.ShowAsync();
                    break;
                case 10:
                    ContentDialog nombreDuplicado = new ContentDialog()
                    {
                        Title = "Nombre duplicado",
                        Content = "Ya existe un proyecto con el mismo nombre",
                        PrimaryButtonText = "Aceptar"
                    };
                    await nombreDuplicado.ShowAsync();
                    break;

                case 11:
                    ContentDialog textoVacio = new ContentDialog()
                    {
                        Title = "Nombre no valido",
                        Content = "El nombre debe tener al menos 3 caracteres",
                        PrimaryButtonText = "Aceptar"
                    };
                    await textoVacio.ShowAsync();
                    break;

                case 12:
                    ContentDialog yaAsignada = new ContentDialog()
                    {
                        Title = "La etiqueta existe",
                        Content = "El fragmento seleccionado ya ha sido asignado a la misma etiqueta",
                        PrimaryButtonText = "Aceptar"
                    };
                    await yaAsignada.ShowAsync();
                    break;

            }
        }
        //internal class ProgressBarWrapper : IProgressIndicator
        //{
        //    private readonly ProgressBar m_ProgressBar;

        //    public ProgressBarWrapper(ProgressBar toolStripProgressBar)
        //    {
        //        m_ProgressBar = toolStripProgressBar;
        //    }

        //    public Double Value
        //    {
        //        get { return m_ProgressBar.Value; }
        //        set { m_ProgressBar.Value = value; }
        //    }

        //    public virtual Double Maximum
        //    {
        //        get { return m_ProgressBar.Maximum; }
        //        set { m_ProgressBar.Maximum = value; }
        //    }

        //    public virtual void Increment(int value)
        //    {
        //        // m_ProgressBar.Increment(value);
        //        // Application.DoEvents();
        //    }
        //}
        private void lvEtiquetas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            etiqueta = (Etiqueta)lvEtiquetas.SelectedItem;
        }
        private void CerrarProyecto(object sender, RoutedEventArgs e)
        {
            proyecto = null;

            this.Frame.Navigate(typeof(ProyectosPage));
        }
             private void btEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AceptarAsignarEtiqueta(object sender, RoutedEventArgs e)
        {
            if (lvAsignarEtiquetas.SelectedItem != null)
            {
                etiqueta = (Etiqueta)lvAsignarEtiquetas.SelectedItem;
            }
            else
            {
                Debug.WriteLine("No hay eti");
            }
            Windows.UI.Text.ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                string seleccion;
                selectedText.GetText(TextGetOptions.None, out seleccion);
                cita = new Cita();
                citaVM = new CitaViewModel(etiqueta);
                cita.DocumentoId = documento.Id;
                cita.Texto = seleccion;
                bool existe = false;
                if (etiqueta != null && citaVM.citas != null && citaVM.citas.Count > 0)
                {
                    foreach (Cita c in citaVM.citas)
                    {
                        if (c.Texto.Equals(seleccion))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (!existe)
                {
                    cita.EtiquetaId = etiqueta.Id;
                    citaVM.AgregarCita(cita);
                    etiqueta.NumCitas = etiqueta.NumCitas + 1;
                    documento.Citas.Add(cita);
                    etiqVM.ActualizarEtiqueta(etiqueta);
                    documentoVM.ActualizarDocumento(documento);
                    Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                    if (charFormatting.Underline == Windows.UI.Text.UnderlineType.None)
                    {
                        charFormatting.Underline = Windows.UI.Text.UnderlineType.Single;
                    }
                    else
                    {
                        charFormatting.Underline = Windows.UI.Text.UnderlineType.None;
                    }
                    selectedText.CharacterFormat = charFormatting;
                    MostrarDialog(4);
                }
                else
                {
                    MostrarDialog(12);
                }
            }
            else
            {
                MostrarDialog(5);
            }
        }
        private void EliminarProyecto(object sender, RoutedEventArgs e)
        {

            try
            {
                if (lvPEliminar.SelectedItem != null)
                {
                    Proyecto p = (Proyecto)lvPEliminar.SelectedItem;

                    proyectoVM.EliminarProyecto(p);
                    flyLPEliminar.Hide();
                    this.Frame.Navigate(typeof(ProyectosPage));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No se puede elimnar el proyecto");
            }
        }

        private void AceptarNuevoNombre_Click(object sender, RoutedEventArgs e)
        {
            string textoIng = tbCambiarNombre.Text;
            if (textoIng != null && textoIng.Length > 3)
            {
                proyecto.Nombre = textoIng;
                proyectoVM.ActualizarProyecto(proyecto);
                flyCambiarNombre.Hide();
                this.Frame.Navigate(typeof(ProyectosPage));
            }
            else
            {
                MostrarDialog(11);
            }

        }

        private void lvDocumentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
