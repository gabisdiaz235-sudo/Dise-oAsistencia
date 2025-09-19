using Microsoft.UI.Xaml;
using ModeloAsistencia;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using ModeloAsistencia.Context;
using SistemaGestionAsistencia.Navegacion;
using SistemaGestionAsistencia.Vistas;
using SistemaGestionAsistencia.VistaModelo;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SistemaGestionAsistencia
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        private Dictionary<Type, Type> viewMappings = new Dictionary<Type, Type>
    {
        { typeof(RegistrarUsuarioViewModel), typeof(VistaRegistrarUsuario) },
        // Agrega más aquí según sea necesario
        };

        private Frame GetRootFrame()
        {
            if (Window.Current.Content is Frame contentFrame)
            {
                return contentFrame;
            }

            return null;
        }

        public void NavigateTo(Type viewModelType)
        {
            if (viewMappings.TryGetValue(viewModelType, out Type viewType))
            {
                // Implementa la lógica de navegación para mostrar la vista correspondiente
                var contentFrame = GetRootFrame();
                if (contentFrame != null)
                {
                    contentFrame.Navigate(viewType);
                }
            }
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            //WindowInicioSesion sesion = new WindowInicioSesion();
            //sesion.Activate();

            m_window = new MainWindow();
            m_window.Activate();



            using (var context = new ModeloContext())
            {
                context.Database.EnsureCreated();
            }
        }

        private Window m_window;
    }
}
