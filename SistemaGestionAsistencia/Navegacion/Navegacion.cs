using Frame = Microsoft.UI.Xaml.Controls.Frame;
using SistemaGestionAsistencia.Commands;
using SistemaGestionAsistencia.Vistas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using ModeloAsistencia.Modelo;
using SistemaGestionAsistencia.VistaModelo;

namespace SistemaGestionAsistencia.Navegacion
{
    public static class ClassNavegacion
    {
        private static Frame _contentFrame;

        public static void Inicializar(Frame frame)
        {
            _contentFrame = frame;
        }

        public static void MostrarPagina(Type pagina)
        {
            if (_contentFrame != null)
            {
                _contentFrame.Navigate(pagina);
            }
        }
        public static void MostrarPaginaConParametro(Type pagina, object parametro)
        {
            if (_contentFrame != null)
            {
                _contentFrame.Navigate(pagina);
            }
        }

        public static void Regresar()
        {
            if (_contentFrame != null && _contentFrame.CanGoBack)
            {
                _contentFrame.GoBack();
            }
        }
    }
}

