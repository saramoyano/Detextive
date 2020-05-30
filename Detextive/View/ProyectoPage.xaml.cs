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
                Debug.WriteLine(proyecto.Id);
                openFileButton.IsEnabled = true;
                saveFileButton.IsEnabled = true;
                underlineButton.IsEnabled = true;
                btnEtiqueta.IsEnabled = true;
                btnNube.IsEnabled = true;
                btnCerrarProy.IsEnabled = true;
                btnProyecto.IsEnabled = false;
                btnAbrirProy.IsEnabled = false;
                Debug.WriteLine("proyectoId: " + proyecto.Id);
                etiqVM = new EtiquetaViewModel(proyecto);
                Debug.WriteLine(etiqVM == null);
                documentoVM = new DocumentoViewModel(proyecto);
                Debug.WriteLine(documentoVM == null);
                //if (etiqVM.etiquetas != null)
                //{
                //    foreach (Etiqueta etiq in etiqVM.etiquetas)
                //    {
                //        Debug.WriteLine("nombre etiqueta: " + etiq.Nombre);
                //        proyecto.Etiquetas.Add(etiq);
                //    }
                //}

            }

            if (proyecto == null)
            {
                openFileButton.IsEnabled = false;
                saveFileButton.IsEnabled = false;
                underlineButton.IsEnabled = false;
                btnEtiqueta.IsEnabled = false;
                btnNube.IsEnabled = false;
                btnCerrarProy.IsEnabled = false;
                MostrarDialog();
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
                    proyectoVM.AgregarProyecto(proyecto);
                    flyNombre.Hide();
                    Proyecto_Ok();

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

            Debug.WriteLine("proyectoId: "+proyecto.Id);
           etiqVM = new EtiquetaViewModel(proyecto);
            ////Debug.WriteLine(etiqVM == null);
           documentoVM = new DocumentoViewModel(proyecto);
            ////Debug.WriteLine(documentoVM == null);
            //if (etiqVM.etiquetas != null)
            //{
            //    foreach (Etiqueta etiq in etiqVM.etiquetas)
            //    {
            //        Debug.WriteLine("nombre etiqueta: " + etiq.Nombre);
            //        proyecto.Etiquetas.Add(etiq);
            //    }
            //}
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
                    Debug.WriteLine("etiqueta proyId: " + proyecto.Id);
                    etiqueta.IdProy = proyecto.Id;
                    // etiqueta.Proyecto.Id = proyecto.Id;
                    Debug.WriteLine("etiqueta nombre: " + textoIngresado);
                    etiqueta.Nombre = textoIngresado;
                    etiqueta.NumCitas = 0;
                    flyNvaEtiqueta.Hide();
                    Debug.WriteLine(etiqVM == null);
                    etiqVM.AgregarEtiqueta(etiqueta);
                    proyecto.Etiquetas.Add(etiqueta);
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
                if (file.FileType == ".rtf"  )
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
                        ContentDialog errorDialog = new ContentDialog()
                        {
                            Title = "File open error",
                            Content = excepcion.Message,
                            PrimaryButtonText = "Ok"
                        };

                        await errorDialog.ShowAsync();
                    }
                }

                try
                {

                    editor.Document.GetText(TextGetOptions.None, out texto);
                   
                    //string[] archivo = file.Path.Split( "\\");
                    //String ruta = archivo[0].Substring(0, 1);
                    //foreach (string a in archivo) {
                    //    ruta = ruta + "\\" + a;
                    //}
                    string[] nombre = file.Name.Split(".");
                    string name = nombre[0];
                    //Debug.WriteLine(ruta);

                    documento = new Documento();
                    documento.Ubicacion = "ruta";
                    documento.IdProy = proyecto.Id;
                  //  documento.Proyecto.Id = proyecto.Id;
                    // Debug.WriteLine(name);
                    documento.Nombre = name;
                    //Debug.WriteLine(texto.Length);
                    documento.Extension = (int)texto.Length; 
                    Debug.WriteLine("existe doc: "+ documentoVM.ExisteDocumento(documento, proyecto));
                    if (!documentoVM.ExisteDocumento(documento, proyecto)) {

                        documentoVM.AgregarDocumento(documento);

                        proyecto.Documentos.Add(documento);
                    }


                    UpdateWords();
                }
                catch (Exception excepcion)
                {

                    ContentDialog errorDialog = new ContentDialog()
                    {
                        Title = "Error",
                        Content = excepcion.Message,
                        PrimaryButtonText = "Ok"
                    };

                    await errorDialog.ShowAsync();
                }

                //CreateCloud(new FileInfo(file.Path));


                //       rebArchivo.;



                //Windows.Storage.StorageFolder localStorage = Windows.Storage.ApplicationData.Current.LocalFolder;
                //StorageFile copiedFile = await file.CopyAsync(localStorage, file.Name, NameCollisionOption.GenerateUniqueName);
                //try
                //{

                //    //webView1.Source = new Uri(string.Format("ms-appdata:///pdfjs/web/viewer.html?file={0}", string.Format("ms-appdata:///Documentos/{0}", WebUtility.UrlEncode("Tema01 - Practica01.pdf"))));
                //}
                //catch (Exception e) {
                //    ContentDialog error = new ContentDialog
                //    {
                //        Title = "Error",
                //        Content = e.Message,
                //        CloseButtonText = "Ok"
                //    };
                //}
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
        {
            Windows.UI.Text.ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                cita = new Cita();
                cita.IdDoc = documento.Id;
                cita.Texto = selectedText.ToString();
                CitaViewModel citaVM = new CitaViewModel();
                citaVM.AgregarCita(cita);

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
            }
        }
        private async void MostrarDialog()
        {
            ContentDialog proyDialog = new ContentDialog()
            {
                Title = "Comenzar",
                Content = "Para comenzar a trabajar, cree un nuevo proyecto o abra uno existente",
                PrimaryButtonText = "Entendido"
            };
            ContentDialogResult resultado = await proyDialog.ShowAsync();
        }
        private void BaseExample_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void UpdateWords()
        {
            Debug.WriteLine("Entra aqui");
            string textoNuevo = texto;
            if (textoNuevo.Length < 3)
            {
                return;
            }

            IEnumerable<string> terms = new StringExtractor(textoNuevo, _progress);

            

            IEnumerable<IWord> words = terms.CountOccurences().SortByOccurences().Cast<IWord>();
            
            CloudControl.WeightedWords =
               terms
               .Filter(_blacklist)
               .CountOccurences()
                   .SortByOccurences();

            foreach (var item in words)
            {
               

                palabras.Add(item.Text);
                frecuencias.Add(item.Occurrences);
            }

            //nube = new Nube();
            //nube.IdProy = proyecto.Id;
            //nube.NumDocumentos = "1";
            //nube.ExtensionFragmento = texto.Length;
            //nube.Proyecto = proyecto;
            //Debug.WriteLine("doc ID: " + documento.Id);
            //nube.IdDoc = documento.Id;
            //nubeVM = new NubeViewModel();

            int numPal = 0;
            Palabra palabra;
            try
            {
                //foreach (var term in terms)
                //{
                //    numPal++;
                //    palabra = new Palabra();
                //    palabra.IdProy = proyecto.Id;
                //   // palabra.Proyecto = proyecto;
                //    palabra.IdNube = nube.Id;
                // //   palabra.Nube = nube;
                //    palabra.Nombre = term;
                //    palabra.NumApariciones = 1;                    
                //    palabra.Porcentaje = 1;

                //    palabraVM = new PalabraViewModel();
                //    palabraVM.AgregarPalabra(palabra);
                //    nube.PalabrasSet.Add(palabra);
                //}
                //nube.NumConceptos = numPal;
                //nubeVM.AgregarNube(nube);
                //proyecto.Nubes.Add(nube);
            }
            catch (Exception ex)
            {
               // Debug.WriteLine(ex.StackTrace);
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

        }
        private void CerrarProyecto(object sender, RoutedEventArgs e)
        {
            proyecto = null;
            openFileButton.IsEnabled = false;
            saveFileButton.IsEnabled = false;
            underlineButton.IsEnabled = false;
            btnEtiqueta.IsEnabled = false;
            btnNube.IsEnabled = false;
            btnCerrarProy.IsEnabled = false;
            btnAbrirProy.IsEnabled = true;
            btnProyecto.IsEnabled = true;
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
                "dos" , "en" ,
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
                "etc" ,   "ha" ,"haces" ,
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
                "os" , "para" , "pero" ,"podéis" ,
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
                "quizá" , "se" ,
                "según" ,
                "ser" ,
                "si" ,
                "sí" ,
                "siempre" ,
                "siendo" ,
                "sin" ,
                "sino" ,
                "so" ,
                "sobre" ,
                "sois" ,
                "solamente" ,
                "solo" ,
                "sólo" , "sr" ,
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
                "vaya" , "voy" ,"y" ,
                "ya" ,
                "yo",};

            public BannedWords() : base(s_TopCommonWords)
            {
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

