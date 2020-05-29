﻿using AccesoDatos.Model;
using Detextive.ViewModel;
using iText.Layout;
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
    public sealed partial class DocumentoPage : Page
    {
        Proyecto proyecto;
        public DocumentoViewModel documentoVM;
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
            }
        }

        private void lvDocs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            documento = (Documento)lvDocs.SelectedItem;



        }

        private void Eliminar_Documento(object sender, RoutedEventArgs e)
        {

            documentoVM.EliminarDocumento(documento);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProyectoPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Detextive.View.ProyectoPage), proyecto);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        public void btDocPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btEtiPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btNubePage_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btNubePage_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btEtiPage_Click_1(object sender, RoutedEventArgs e)
        {
this.Frame.Navigate(typeof(Detextive.View.EtiquetaPage), proyecto);
        }

        private void btDocPage_Click_1(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void btNubePage_Click_2(object sender, RoutedEventArgs e)
        {
this.Frame.Navigate(typeof(Detextive.View.NubePages), proyecto);
        }
    }
}
