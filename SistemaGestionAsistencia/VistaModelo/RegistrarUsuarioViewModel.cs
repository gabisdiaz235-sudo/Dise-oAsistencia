using Microsoft.UI.Xaml.Controls;
using SistemaGestionAsistencia.CarpetaSQL;
using SistemaGestionAsistencia.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SistemaGestionAsistencia.Vistas;
using ModeloAsistencia.Modelo;
using System.Diagnostics;
using DateTimeOffset = System.DateTimeOffset;
using SistemaGestionAsistencia.Navegacion;
using Catel.Services;
using Microsoft.UI.Xaml;

namespace SistemaGestionAsistencia.VistaModelo
{
    internal class RegistrarUsuarioViewModel : ViewModelBase
    {
        private readonly BD bd;
        private ObservableCollection<Empleado> _datos;
        private Empleado _dato;
        private Pais _paisSeleccionado;
        private Estados _estadoSeleccionado;
        private DateTimeOffset? _fechaNacimiento2;
        private ObservableCollection<Pais> _paises;
        private ObservableCollection<Estados> _estados;
        


        public RegistrarUsuarioViewModel()
        {
            bd = new BD();
            _dato = new Empleado();
            _paises = bd.GetPaises();
            _datos = bd.Get();
            
        }

        //------------------------------------------------
        public Empleado Dato
        {
            get => _dato;
            set
            {
                if (_dato != value)
                {
                    _dato = value;
                    OnPropertyChanged(nameof(Dato));
                    
                }
            }
        }
        public Pais PaisSeleccioando
        {
            get => _paisSeleccionado;
            set
            {
                if (_paisSeleccionado != value)
                {
                    _paisSeleccionado = value;
                    OnPropertyChanged(nameof(PaisSeleccioando));
                    _estados = bd.GetEstadosPorPais(PaisSeleccioando.PaisID);
                    OnPropertyChanged(nameof(Estados));
                }
            }
        }
        public Estados EstadoSeleccionado
        {
            get => _estadoSeleccionado;
            set
            {
                if (_estadoSeleccionado != value)
                {
                    _estadoSeleccionado = value;
                    OnPropertyChanged(nameof(EstadoSeleccionado));

                }
            }
        }
        public DateTimeOffset? FechaNacimiento2
        {
            get => _fechaNacimiento2;
            set
            {
                if (_fechaNacimiento2 != value)
                {
                    _fechaNacimiento2 = value;
                    OnPropertyChanged(nameof(FechaNacimiento2));
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
        public ObservableCollection<Pais> Paises
        {
            get { return _paises; }
            set
            {
                if (_paises != null)
                {
                    _paises = value;
                    OnPropertyChanged(nameof(Paises));
                }
                
            }
        }
        public ObservableCollection<Estados> Estados
        {
            get { return _estados; }
            set
            {
                if( _estados != value) 
                {
                    _estados = value;
                    OnPropertyChanged(nameof(Estados));
                }
                
            }
        }

        //------------------Command--------------------------
        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(AddExecute, AddCanExecute);
            }
        }
        private void AddExecute(object empleado)
        {
            Debug.WriteLine(FechaNacimiento2.ToString() + "--------------------------------------------------");
            DateTime dateTime = FechaNacimiento2.HasValue ? FechaNacimiento2.Value.DateTime : DateTime.MaxValue;
            Dato.FechaNacimiento = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            Debug.WriteLine(Dato.FechaNacimiento.ToString() + "--------------------------------------------------");
            Dato.Pais = PaisSeleccioando.PaisNombre.ToString();
            Dato.Estado = EstadoSeleccionado.EstadoNombre.ToString();

            bd.Add(Dato);
            Datos = bd.Get();
            LimpiarCampos();
        }
        private bool AddCanExecute(object empleado)
        {
            return true;
        }
        //------------------Funciones--------------------------
        private void LimpiarCampos()
        {
            // Restablece los campos de la clase Cliente o los valores utilizados en la interfaz de usuario
            _dato = new Empleado();  // Esto crea una nueva instancia de Cliente, asegurándose de que los campos estén vacíos
            OnPropertyChanged(nameof(Dato));  // Asegura que la interfaz de usuario se actualice con los nuevos valores
        }
        //---------------------------------------------------------------------------------------------
    }
}
