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
using StopWord;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

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
        List<string> nombres;
        string texto;
        public ProyectoPage()
        {
            this.InitializeComponent();
            proyectoVM = new ProyectoViewModel();
         
            palabras = new List<string>();
            frecuencias = new List<int>();
            nombres = new List<string>();
            _blacklist = new BannedWords();// CommonWords();
            _progress = new ProgressBarWrapper(ProgressBar);

             
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            openFileButton.IsEnabled = false;
            saveFileButton.IsEnabled = false;
            underlineButton.IsEnabled = false;
            btnEtiqueta.IsEnabled = false;
            btnNube.IsEnabled = false;
            btnCerrarProy.IsEnabled = false; 
            etiqVM = new EtiquetaViewModel();
            MostrarDialog();
            
           
         
             // spEtiq.Children.Add(lvEtiq);
            }


        private void NuevoProyecto(object sender, RoutedEventArgs e)
        {}


        private void AceptaNombreProyecto(object sender, RoutedEventArgs e)
        {
            string textoIngresado = textBoxProy.Text;
            if (textoIngresado != "")
            {
               
                try
                {
                    proyecto = new Proyecto();
                    proyecto.Nombre = textoIngresado;
                    
                    flyNombre.Hide();
                    proyectoVM.AgregarProyecto(proyecto);
                    openFileButton.IsEnabled = true;
                    saveFileButton.IsEnabled = true;
                    underlineButton.IsEnabled = true;
                    btnEtiqueta.IsEnabled = true;
                    btnNube.IsEnabled = true;
                    btnProyecto.IsEnabled = false;
                    btnAbrirProy.IsEnabled = false;
                    foreach (Etiqueta eti in etiqVM.ListaEtiquetas())
                    {
                        nombres.Add(eti.Nombre);
                    }

                    //ListView lvEtiq = new ListView();
                    lvEtiquetas.ItemsSource = nombres;
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
                    etiqueta.IdProy = proyecto.Id;
                    etiqueta.Nombre = textoIngresado;
                    etiqueta.NumCitas = 0;

                    flyNvaEtiqueta.Hide();
                    etiqVM.AgregarEtiqueta(etiqueta);
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

        private void CrearNubes(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Detextive.View.DocumentoPage));

        }


        public async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".pdf");
            picker.FileTypeFilter.Add(".doc");
            picker.FileTypeFilter.Add(".rtf");


            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {

                string archivo = file.Path;
                documento = new Documento();
                documento.Ubicacion = archivo;
         
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

                    editor.Document.GetText(TextGetOptions.FormatRtf, out texto);

                    //string creado para ver si estaba teniendo problemas de rtf
                    //texto = "“No somos mojigatos ni pretendemos antiguallas”. Sexualidad y pareja en el discurso del Movimiento Familiar Cristiano de Córdoba" +
                        //" en las décadas del sesenta y setenta. Asistimos como meros espectadores, acostumbrados ya a tanta injusticia, a tanto odio," +
                        //" que más de una vez nos sirven para “justificar” nuestras propias faltas e injusticias[…] Si vamos al fondo de la cuestión deberemos" +
                        //" concluir que la injusticia, la tortura, el asesinato, etc., no son más que el fruto de un proceso de deshumanización creciente, " +
                        //"cuyo punto de partida está en la familia, o mejor dicho en la NO FAMILIA. Sí, porque todos[…] somos en definitiva responsables de que " +
                        //"el hombre esté perdiendo –insensiblemente - el sentido de lo humano y en consecuencia –también insensiblemente, poco a " +
                        //"poco-transformándose y actuando como una bestia. El fragmento precedente expresa con claridad la que fuera una de las preocupaciones " +
                        //"centrales en las reflexiones que muchos actores hacían de su época. Los cambios familiares eran vistos al mismo tiempo como " +
                        //"posibilidad o amenaza, las posturas que abogaban por transformar la sociedad eran discutidas por los diagnósticos de crisis y " +
                        //"desintegración social, que reconocían como causas a las transformaciones familiares. Estos debates no eran novedosos, ya que como" +
                        //" elemento fundamental de la sociedad, la familia ha sido objeto de la acción del Estado y la Iglesia, suscitando acuerdos y conflictos." +
                        //"A pesar de la multiplicidad de formas existentes, la difusión de un modelo de organización familiar evidencia que ésta revistió " +
                        //"una importancia crucial para el orden social. Como ha señalado Ghirardi, el modelo de familia promovido por la Iglesia a partir del" +
                        //" Concilio de Trento consagraba al matrimonio religioso como el único válido, esta unión debía ser monógama," +
                        //" heterosexual y no podía disolverse(sólo se aceptaba la separación de cuerpos).En la construcción de este ideal destacaba además la " +
                        //"regulación de la sexualidad, que debía restringirse al ámbito conyugal, estar destinada únicamente a la reproducción con el objetivo de" +
                        //" engendrar descendencia legítima y ser estrictamente limitada en el caso de las mujeres, valorizando la virginidad y la castidad." +
                        //" La autoridad del paterfamilias dentro del grupo doméstico constituía otro componente de control social. " +
                        //"Mucho se ha debatido acerca de la influencia de la Iglesia y el Estado sobre las familias." +
                        //"Es necesario destacar que la familia se convirtió, en muchos casos, en un elemento generador de tensiones entre" +
                        //" ambos actores, dado que la regulación de la organización familiar –en términos de relaciones entre sus miembros," +
                        //" roles de género y vínculos intergeneracionales, y como espacio, de conductas y prácticas cotidianas que " +
                        //"involucran aspectos como la sexualidad y la educación - posibilitaría el control sobre realidades diversas, " +
                        //"disímiles y cambiantes en el intento de sostener el orden social y reforzar la importancia de estos actores " +
                        //"en la sociedad.Este modelo se reconfiguró y consolidó a principios del siglo XX, fuertemente asociado al " +
                        //"desarrollo de la clase media.  Ahora bien, los años sesenta fueron escenario de importantes cambios culturales, " +
                        //"sociales y políticos, que afectaron profundamente la vida cotidiana de la población.  La Iglesia no permaneció al " +
                        //"margen de los mismos: inició en 1963, con el Concilio Vaticano II, un proceso de renovación cuyas repercusiones " +
                        //"en Latinoamérica cristalizaron en la Conferencia del Episcopado Latinoamericano en Medellín y la declaración " +
                        //"de los 18 obispos para el Tercer Mundo.En Argentina, todos estos cambios adquirieron matices propios, en la " +
                        //"medida en que el proceso de radicalización – que afectó también a los católicos – fue un elemento central de la " +
                        //"vida social y de la política. Asimismo, las transformaciones sociales, políticas y culturales producidas durante " +
                        //"esta década implicaron cuestionamientos al paradigma de familia imperante. En efecto, la aparición de la píldora," +
                        //" los debates sobre el lugar de la mujer y sobre el control de la natalidad, fueron factores que tendieron a reconfigurar" +
                        //" y resignificar el ideal familiar. Durante los años sesenta, la familia, la moralidad y los valores cristianos," +
                        //" fueron elementos centrales en el discurso de la jerarquía eclesiástica.En el mencionado contexto, la llegada del Concilio Vaticano " +
                        //"II se veía como la posibilidad de una solución a los problemas denunciados por la Iglesia, principalmente el materialismo, el comunismo marxista," +
                        //" el clima de inmoralidad y el peligro de la excesiva búsqueda de gozo. Esta situación, según el arzobispo de Córdoba, implicaba que:" +
                        //" Dos mundos diametralmente opuestos se enfrentan en esta hora decisiva: el reino de Cristo y el poder de las tinieblas. Y en esta lucha " +
                        //"titánica nadie puede ser indiferente, nadie simple espectador y las posiciones neutralistas resultan absurdas; porque o se está con Cristo o contra Cristo." +
                        //"Este y otros discursos dan algunos indicios acerca de los comportamientos familiares y de la familia como ámbito de formación en el catolicismo.Del mismo modo," +
                        //" muestran la importancia que otorgaba la Iglesia a la familia como espacio de formación de católicos. El ámbito doméstico, y en particular, la educación que " +
                        //"los padres daban a sus hijos era un componente fundamental para el desarrollo de una sociedad católica, que ";
                        UpdateWords();
                }
                catch (Exception excepcion)
                {
                    ContentDialog errorDialog = new ContentDialog()
                    {
                        Title = "Error",
                        Content = excepcion.StackTrace,
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

        
        private void AbrirProyecto(object sender, RoutedEventArgs e)
        {
            
            
           
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
            int numPal = 0;
            foreach (string term in terms) {
                numPal++;
            }

            CloudControl.WeightedWords =
                terms
                .Filter(_blacklist)
                    .CountOccurences()
                    .SortByOccurences();

            nube = new Nube();
               nube.IdProy = proyecto.Id;
                 nube.NumDocumentos ="1";
             nube.ExtensionFragmento = texto.Length;
             nubeVM = new NubeViewModel();
            nube.NumConceptos = numPal;
            nubeVM.AgregarNube(nube);
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

        internal class BannedWords : CommonBlacklist
        {
            private static readonly string[] s_TopCommonWords =
                new[]
                {"I"};

            public BannedWords() : base(s_TopCommonWords)
            {
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
