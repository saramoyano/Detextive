using AccesoDatos.Model;
using Detextive.View;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
namespace Detextive
{

    public sealed partial class MainPage : Page
    {
        public string mruToken { get; internal set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void nvMenuPpal_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                NavigationViewItem nvi = (NavigationViewItem)args.SelectedItem;

                if (nvi.Tag.ToString() == "proyecto")
                {
                    this.ContentFrame.Navigate(typeof(ProyectosPage), null, new EntranceNavigationTransitionInfo());
                }
                else if (nvi.Tag.ToString() == "documentos")
                {
                    this.ContentFrame.Navigate(typeof(DocumentoPage), null, new DrillInNavigationTransitionInfo());
                }
                else if (nvi.Tag.ToString() == "etiquetas")
                {
                    this.ContentFrame.Navigate(typeof(EtiquetaPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                }
                else if (nvi.Tag.ToString() == "nubes")
                {
                    this.ContentFrame.Navigate(typeof(NubePages), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                }
            }
        }

        private void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Navigate(typeof(ProyectosPage), null, new EntranceNavigationTransitionInfo());
        } 
    }        
}
 
