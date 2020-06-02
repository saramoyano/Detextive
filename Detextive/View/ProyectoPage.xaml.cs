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

namespace Detextive
{

    public sealed partial class ProyectoPage : Page
    {
        ProyectoViewModel proyectoVM;
        EtiquetaViewModel etiqVM;
        DocumentoViewModel documentoVM;
        NubeViewModel nubeVM;
        CitaViewModel citaVM;
        Proyecto proyecto;
        Etiqueta etiqueta;
        Documento documento;
        Cita cita;
        Nube nube;
        Palabra palabra;

        private IBlacklist _blacklist;
        private IProgressIndicator _progress;
        List<string> palabras;
        List<int> frecuencias;
        //List<string> nombres;
        string texto;
        private PalabraViewModel palabraVM;
        public ProyectoPage()
        {
            this.InitializeComponent();
            proyectoVM = new ProyectoViewModel();

            _blacklist = new BannedWords();
            _progress = new ProgressBarWrapper(ProgressBar);
            palabras = new List<string>();
            frecuencias = new List<int>();
            //nombres = new List<string>();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter.GetType().Equals(typeof(Proyecto)))
            {
                proyecto = (Proyecto)e.Parameter;

                openFileButton.IsEnabled = true;
                saveFileButton.IsEnabled = true;
                underlineButton.IsEnabled = true;
                btnEtiqueta.IsEnabled = true;
                btnNube.IsEnabled = true;
                btnCerrarProy.IsEnabled = true;
                btnProyecto.IsEnabled = false;
                btnAbrirProy.IsEnabled = false;
                btEliminar.IsEnabled = false;
                etiqVM = new EtiquetaViewModel(proyecto);
                documentoVM = new DocumentoViewModel(proyecto);
                nubeVM = new NubeViewModel(proyecto);
                if (proyecto.Documentos != null) ;
                foreach (Documento doc in proyecto.Documentos) {
                    Debug.WriteLine("documento" + doc);
                }
            }

            if (proyecto == null)
            {
                openFileButton.IsEnabled = false;
                saveFileButton.IsEnabled = false;
                underlineButton.IsEnabled = false;
                btnEtiqueta.IsEnabled = false;
                btnNube.IsEnabled = false;
                btnCerrarProy.IsEnabled = false;
                MostrarDialog(1);
            }
        }
        private void NuevoProyecto(object sender, RoutedEventArgs e)
        { }
        private void AceptaNombreProyecto(object sender, RoutedEventArgs e)
        {
            string textoIngresado = textBoxProy.Text;
            if (textoIngresado != "")
            {
                try
                {
                    proyecto = new Proyecto();
                    proyecto.Nombre = textoIngresado;
                    if (!proyectoVM.ExisteProyecto(proyecto))
                    {
                        proyectoVM.AgregarProyecto(proyecto);
                        Proyecto_Ok();
                        flyNombre.Hide();
                    }
                    else
                    {
                        ContentDialog proyDialog = new ContentDialog()
                        {
                            Title = "Nombre duplicado",
                            Content = "Ya existe un proyecto con el mismo nombre",
                            PrimaryButtonText = "Aceptar"
                        };
                        proyDialog.ShowAsync();
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
        }
        private void Proyecto_Ok()
        {
            etiqVM = new EtiquetaViewModel(proyecto);
            documentoVM = new DocumentoViewModel(proyecto);
            this.Frame.Navigate(typeof(ProyectoPage), proyecto);
        }
        private void AbrirProyecto(object sender, RoutedEventArgs e)
        {

        }
        private void lvProyectos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            proyecto = (Proyecto)lvProyectos.SelectedItem;
            Debug.WriteLine(proyecto.Id);
            flyListaProy.Hide();
            Proyecto_Ok();
        }
        private void CrearEtiqueta(object sender, RoutedEventArgs e)
        {

        }
        private void AceptarEtiqueta(object sender, RoutedEventArgs e)
        {
            string textoIngresado = tbEtiqueta.Text;
            if (textoIngresado != "")
            {
                try
                {
                    etiqueta = new Etiqueta();
                    etiqueta.ProyectoId = proyecto.Id;
                    etiqueta.Nombre = textoIngresado;
                    etiqueta.NumCitas = 0;
                    flyNvaEtiqueta.Hide();
                    bool existe = etiqVM.ExisteEtiqueta(etiqueta, proyecto);
                    if (!existe)
                    {
                        etiqVM.AgregarEtiqueta(etiqueta);
                        proyecto.Etiquetas.Add(etiqueta);
                        MostrarDialog(6);
                    }
                    else
                    {
                        MostrarDialog(7);
                    }
                }
                catch (Exception excepcion)
                {
                    ContentDialog error = new ContentDialog
                    {
                        Title = "Hubo un problema",
                        Content = excepcion.StackTrace,
                        CloseButtonText = "Aceptar"
                    };
                }
            }
        }
        private void CrearNubes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.DocumentoPage));
        }
        public async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".rtf");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                if (file.FileType == ".rtf")
                {
                    try
                    {
                        Windows.Storage.Streams.IRandomAccessStream randAccStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                        // Load the file into the Document property of the RichEditBox.
                        editor.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);

                    }
                    catch (Exception excepcion)
                    {
                        MostrarDialog(9);
                    }
                }

                try
                {
                    editor.Document.GetText(TextGetOptions.None, out texto);

                    string[] nombre = file.Name.Split(".");
                    string name = nombre[0];
                    if ((!documentoVM.ExisteDocumento(name, proyecto)))
                    {
                        documento = new Documento();
                        documento.Ubicacion = file.Path;
                        documento.ProyectoId = proyecto.Id;
                        documento.Nombre = name;
                        documentoVM.AgregarDocumento(documento);
                        //proyecto.Documentos.Add(documento);           
                        UpdateWords();
                    
                    }
                    else
                    {
                        documento = documentoVM.GetDocumento(name);
                    }
                }
                catch (Exception excepcion)
                {
                    MostrarDialog(8);
                }
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
            }
        }
        private void BaseExample_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void UpdateWords()
        {
            string textoNuevo = texto;
            if (textoNuevo.Length < 3)
            {
                return;
            }
            IEnumerable<string> terms = new StringExtractor(textoNuevo, _progress);
            int extension = terms.Count();
           CloudControl.WeightedWords =
               terms
               .Filter(_blacklist)
               .CountOccurences()
               .SortByOccurences();

            if (!nubeVM.ExisteNube(documento, proyecto))
            {
                nube = new Nube();
                nube.NumDocumentos = "1";                
                nube.ExtensionFragmento = extension;                
                documento.Extension = extension;
                nube.ProyectoId = proyecto.Id;
                nube.DocumentoId = documento.Id;                 
                nube.NumConceptos = CloudControl.WeightedWords.Count();                 
                nubeVM.AgregarNube(nube);
            }
            else
            {
                nube = nubeVM.GetNubeFiltro(documento, proyecto);
            }
            Palabra palabra;
            palabraVM = new PalabraViewModel();
            try
            {
                foreach (var item in CloudControl.WeightedWords)
                {
                    palabra = new Palabra();
                    palabra.ProyectoId = proyecto.Id;
                    palabra.NubeId = nube.Id;
                    palabra.Nombre = item.Text;
                    palabra.NumApariciones = item.Occurrences;
                    palabra.Porcentaje = palabra.NumApariciones / nube.ExtensionFragmento;
                    palabraVM.AgregarPalabra(palabra);
                    // nube.PalabrasSet.Add(palabra);
                }
                nubeVM.ActualizarNube(nube);
                //   proyecto.Nubes.Add(nube);
                documentoVM.ActualizarDocumento(documento);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
        internal class ProgressBarWrapper : IProgressIndicator
        {
            private readonly ProgressBar m_ProgressBar;

            public ProgressBarWrapper(ProgressBar toolStripProgressBar)
            {
                m_ProgressBar = toolStripProgressBar;
            }

            public Double Value
            {
                get { return m_ProgressBar.Value; }
                set { m_ProgressBar.Value = value; }
            }

            public virtual Double Maximum
            {
                get { return m_ProgressBar.Maximum; }
                set { m_ProgressBar.Maximum = value; }
            }

            public virtual void Increment(int value)
            {
                // m_ProgressBar.Increment(value);
                // Application.DoEvents();
            }
        }
        private void lvEtiquetas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            etiqueta = (Etiqueta)lvEtiquetas.SelectedItem;
        }
        private void CerrarProyecto(object sender, RoutedEventArgs e)
        {
            //proyecto = null;
            //openFileButton.IsEnabled = false;
            //saveFileButton.IsEnabled = false;
            //underlineButton.IsEnabled = false;
            //btnEtiqueta.IsEnabled = false;
            //btnNube.IsEnabled = false;
            //btnCerrarProy.IsEnabled = false;
            //btnAbrirProy.IsEnabled = true;
            //btnProyecto.IsEnabled = true;

            this.Frame.Navigate(typeof(ProyectoPage));
        }
        public void btDocPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.DocumentoPage), proyecto);
        }
        private void btEtiPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.EtiquetaPage), proyecto);
        }
        private void btNubePage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.NubePages), proyecto);
        }
        private void btSalirPage_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
        private void btProyPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProyectoPage));
        }
        internal class BannedWords : CommonBlacklist
        {
            private static readonly string[] s_TopCommonWords =
                new[]
                {"I", "a" ,
              "acá" ,
              "ahí" , "de", "la", "los", "las", "que", "el", "por", "se", "al" ,
                "algo" ,
                "algún" ,
                "alguna",
                "alguno",
                "algunas",
                "algunos",
                "allá" ,
                "allí" ,
                "ambos" ,
                "ante" ,  "aquí" ,
                "arriba" ,
                "así" ,
                "atrás" ,
                "aun" ,
                "aunque" ,
                "bajo" ,
                "bastante" ,
                "bien" ,
                "cabe" ,
                "cada" ,
                "casi" , "ciertos" ,
                "ciertas" ,
                "como" ,
                "con" ,
                "conmigo" ,
                "conseguimos" ,
                "conseguir" ,
                "consigo" ,
                "consigue" ,
                "consiguen" ,
                "consigues" ,
                "contigo" ,  "cualquieras" ,
                "cualesquiera" ,
                "cuan" ,
                "cuando" ,
                "cuanto" ,
                "cuanta" ,
                "cuantos" ,
                "cuantas" ,
                "de" ,
                "dejar" ,
                "del" ,
                  "desde" ,
                "donde" ,
                "dos" ,
                    "el",
                    "en" ,
                "encima" ,
                "entonces" ,
                "entre" ,
                "era",
                "eras" ,
                "eramos" ,
                "eran" ,
                "eres" ,
                "es" ,
                "esos" ,
                "esa" ,
                "eso" ,
                "ese" ,
                "esas" ,
                "estas" ,
                "esta" ,
                "estaba" ,
                "estado" ,
                "estáis" ,
                "estamos" ,
                "están" ,
                "estar" ,
                "este" ,
                "esto" ,
                "estos" ,
                "estés" ,
                "estoy" ,
                "etc" ,   "ha" ,
                    "han",
                    "haces" ,
                "hace" ,
                "hacéis" ,
                "hacemos" ,
                "hacen" ,
                "hacer" ,
                "hacia" ,
                "hago" ,
                "hasta" ,
                "incluso" ,
                "intentas" ,
                "intenta" ,
                "intentáis" ,
                "intentamos" ,
                "intentan" ,
                "intentar" ,
                "intento" ,
                "ir" ,
                "jamás" ,
                "juntos" ,
                "junto" ,
                "la" ,
                "lo" ,
                "las" ,
                "los" ,
                "largo" ,
                "más" ,
                "mas" ,
                "me" ,
                "menos" ,
                "mi" ,
                "mis" ,
                "mientras" ,
                "mío" ,
                "mía" ,
                "mías" ,
                "míos" ,  "modo" ,
                "mucha" ,
                "muchas" ,
                "muchos" ,
                "muchísima" ,
                "muchísimo" ,
                "muchísimas" ,
                "muchísimos" ,
                "mucho" ,
                "muchos" ,
                "muy" ,
                "nada" ,
                "ni" ,
                "ningún" ,
                "ninguna" ,
                "ninguno" ,
                "no" ,
                "nos" , "nunca" ,
                "os" ,
                    "otras",
                    "para" , "pero" ,"podéis" ,
                "podemos" ,
                "poder" ,
                "podría" ,
                "podrías" ,
                "podríais" ,
                "podríamos" ,
                "podrían" ,
                "por" ,
                "por qué" ,
                "porque" ,  "pueden" ,
                "puedes" ,
                "puede" ,
                "puedo" ,
                "pues" ,
                "que" ,
                "qué" ,
                "querer" ,
                "quienes" ,
                "quien" ,
                "quién" ,
                "quienesquiera" ,
                "quienquiera" ,
                "quizás" ,
                "quizá" ,
                    "se" ,
                "son",
                    "un",
                    "le",
                    "dijo",
                "según" ,
                "ser" ,
                "si" ,
                "sí" ,
                "sido",
                "siempre" ,
                "siendo" ,
                "sin" ,
                "sino" ,
                "so" ,
                "sobre" ,
                "sois" ,
                "solamente" ,
                "solo" ,
                "sólo" ,
                    "sr" ,
                "sra" ,
                "sres" ,
                "sta" ,
                "su" ,
                "sus" ,
                "suya" ,
                "suyo" ,
                "suyas" ,
                "suyos" ,
                "tal" ,
                "tales" ,
                "también" ,
                "tampoco" ,
                "tan" ,
                "tanta" ,
                "tanto" ,
                "tantas" ,
                "tantos" ,
                "te" ,
                "tenéis" ,
                "tenemos" ,
                "tener" ,
                "tengo" ,
                "ti" ,  "tú" ,
                "tu" ,
                "tus" ,
                "tuya " ,
                "tuyo" ,
                "tuyas" ,
                "tuyos" ,     "un " ,
                "una" ,
                "uno" ,
                "unas" ,
                "unos" ,
                "usa" ,
                "usas" ,
                "usáis" ,
                "usamos" ,
                "usan" ,
                "usar" ,
                "uso" ,
                "ustedes" ,
                "usted" ,
                "va" ,
                "van" ,
                "vais" ,
                "valor" ,
                "vamos" ,
                "varias" ,
                "varios" ,
                "vaya" , 
                    "voy" ,
                    "y" ,
                "ya" ,
                "yo",};

            public BannedWords() : base(s_TopCommonWords)
            {
            }
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
                citaVM = new CitaViewModel(documento);
                cita.DocumentoId = documento.Id;
                cita.Texto = seleccion;
                if (etiqueta != null)
                {
                    cita.EtiquetaId = etiqueta.Id;
                    citaVM.AgregarCita(cita);
                    //documento.CitasSet.Add(cita);
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
                    MostrarDialog(5);
                }
            }
            
        }
    }
}
//public void CreateCloud(FileInfo fileInfo) {

//    nubeVM = new NubeViewModel();
//    IEnumerable<string> palabras = nubeVM.GenerarNube(fileInfo);
//    List<string> pal = palabras.ToList();

//    ListView ContactsLV = new ListView();
//    ContactsLV.ItemsSource = pal;


//    listaPal.Children.Add(ContactsLV);
//    var wCloud = new WordCloudGen(500, 300);

//    //wCloud.Draw(palabras, );


//  public void CreateCloud(string texto)
//{
//    nube = new Nube();
//    nube.IdProy = proyecto.Id;
//    nube.NumDocumentos ="1";
//    nube.ExtensionFragmento = texto.Length;
//    nubeVM = new NubeViewModel();
// nubeVM.ProcesarTexto(texto);
//palabras = nubeVM.ObtenerPalabras();



////foreach()

//frecuencias = nubeVM.ObtenerFrecuencias();

//ListView ContactsLV = new ListView();
//ContactsLV.ItemsSource = palabras;


//listaPal.Children.Add(ContactsLV);
//var wCloud = new WordCloudGen(500, 300);

//wCloud.Draw(palabras, frecuencias);


//}

