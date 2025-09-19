using ModeloAsistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Windows.UI.Xaml.Data;

namespace SistemaGestionAsistencia
{
    public static class ConfiguracionGlobal
    {
        public static Empleado MensajeGlobal { get; set; }
        public static string ClaveUnica { get; set; }
    }
}
