using Detextive.ViewModel;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Pdf;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Detextive
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ProyectoPage : Page
    {

        ProyectoViewModel proyectoVM;

        public ProyectoPage()
        {
            this.InitializeComponent();
            proyectoVM = new ProyectoViewModel();
            
        }


        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

           
            
            ////TextBox textBox = new TextBox();
            
            ////textBox.PlaceholderText = "Ingrese un nombre para el proyecto";
            ////// Add the TextBox to the visual tree.
            ////centerGrid.Children.Add(textBox);
            ////if (textBox.TextChanged)
            ////{
            ////    string textoIngresado = textBox.Text;

            ////}



            // OpenButton_Click();
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
                MostrarDialog(archivo);
                if (file.FileType == ".rtf")
                {
                    try
                    {
                        Windows.Storage.Streams.IRandomAccessStream randAccStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                        // Load the file into the Document property of the RichEditBox.
                        editor.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
                    }
                    catch (Exception)
                    {
                        ContentDialog errorDialog = new ContentDialog()
                        {
                            Title = "File open error",
                            Content = "Sorry, I couldn't open the file.",
                            PrimaryButtonText = "Ok"
                        };

                        await errorDialog.ShowAsync();
                    }
                }




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

        private async void MostrarDialog(string archivo)
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Archivo abierto",
                    Content = archivo,
                    CloseButtonText = "Ok"
                };

                ContentDialogResult resultado = await noWifiDialog.ShowAsync();


            }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string textoIngresado= textBoxProy.Text;
            if (textoIngresado != "") {
                openFileButton.IsEnabled = true;
                saveFileButton.IsEnabled = true;
                underlineButton.IsEnabled = true;
                
            }
            flyNombre.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void BaseExample_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    }
