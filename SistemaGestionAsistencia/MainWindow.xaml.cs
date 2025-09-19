using Catel.MVVM;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SistemaGestionAsistencia.Navegacion;
using SistemaGestionAsistencia.Servicio_web;
using SistemaGestionAsistencia.Vistas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SistemaGestionAsistencia
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private RecibirDatosSW datosSW;
        private InisioSesionSW sesionSW;
        public MainWindow()
        {
            this.InitializeComponent();
            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            datosSW = new RecibirDatosSW(dispatcherQueue);
            sesionSW = new InisioSesionSW();
            ClassNavegacion.Inicializar(contentFrame);
            nvSample.SelectedItem = RegistrarUsuarios;
        }

        private void nvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;
            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navOptions.IsNavigationStackEnabled = false;
            }

            NavigationView nv = nvSample;
            Type pageType = typeof(Nullable);

            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem.Name == RegistrarUsuarios.Name)
            {
                nv.Header = "Registrar Usuario";
                pageType = typeof(VistaRegistrarUsuario);
            }
            else if (selectedItem.Name == InformacionUsuarios.Name)
            {
                nv.Header = "Información de los Usuarios";
                pageType = typeof(VistaInformacionUsuarios);
            }
            else if (selectedItem.Name == ListaReporteUsuario.Name)
            {
                nv.Header = "Lista de Reporte de Usuario";
                pageType = typeof(VistaListaReporteUsuario);
            }

            _ = contentFrame.Navigate(pageType);
        }
    }
}
