using ModeloAsistencia.Modelo;
using SistemaGestionAsistencia.CarpetaSQL;
using SistemaGestionAsistencia.Navegacion;
using SistemaGestionAsistencia.Vistas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionAsistencia.VistaModelo
{
    internal class InformacionUsuarioViewModel : ViewModelBase
    {
        private readonly BD bd;
        private ObservableCollection<Empleado> _datos;
        private Empleado _selectedRow;
        private DateTimeOffset? _fechaNacimiento2;
        private string _terminoBusqueda;

        public InformacionUsuarioViewModel()
        {
            bd = new BD();
            _datos = bd.Get();
            _terminoBusqueda = string.Empty;
        }

        public Empleado SelectedRow
        {
            get => _selectedRow;
            set
            {
                if (_selectedRow != value)
                {
                    _selectedRow = value;
                    OnPropertyChanged(nameof(SelectedRow));
                    ConfiguracionGlobal.MensajeGlobal = value;
                    ClassNavegacion.MostrarPagina(typeof(VistaModificarUsuario));
                }
            }
        }

        public ObservableCollection<Empleado> Datos
        {
            get => _datos;
            set
            {
                if (_datos != value)
                {
                    _datos = value;
                    OnPropertyChanged(nameof(Datos));

                }
            }
        }


        public string TerminoBusqueda
        {
            get => _terminoBusqueda;
            set
            {
                if (_terminoBusqueda != value)
                {
                    _terminoBusqueda = value;
                    OnPropertyChanged(nameof(TerminoBusqueda));
                    ActualizarResultados();
                }
            }
        }

        private void ActualizarResultados()
        {
            // Filtra los resultados que coinciden con el término de búsqueda
            var resultadosFiltrados = bd.Get()
                .Where(empleado => empleado.Nombre.ToLower().Contains(_terminoBusqueda.ToLower()))
                .ToList();

            // Actualiza la colección de datos
            Datos.Clear();
            foreach (var resultado in resultadosFiltrados)
            {
                Datos.Add(resultado);
            }
        }
    }
}
