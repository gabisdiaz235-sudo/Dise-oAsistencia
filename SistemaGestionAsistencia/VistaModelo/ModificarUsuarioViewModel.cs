using Catel.MVVM;
using ModeloAsistencia.Modelo;
using SistemaGestionAsistencia.CarpetaSQL;
using SistemaGestionAsistencia.Commands;
using SistemaGestionAsistencia.Navegacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace SistemaGestionAsistencia.VistaModelo
{
    class ModificarUsuarioViewModel : ViewModelBase
    {
        private readonly BD bd;
        private ObservableCollection<Empleado> _datos;
        private ObservableCollection<Pais> _paises;
        private ObservableCollection<Estados> _estados;
        private Empleado _dato;
        private DateTimeOffset? _fechaNacimiento2;
        private Pais _paisSeleccionado;
        private Estados _estadoSeleccionado;

        public ModificarUsuarioViewModel()
        {
            bd = new BD();
            _dato = new Empleado();
            _estadoSeleccionado = new Estados();
            _paisSeleccionado = new Pais();
            _datos = bd.Get();
            _paises = bd.GetPaises();
            _dato = ConfiguracionGlobal.MensajeGlobal;
            if(_dato != null)
            {
                _fechaNacimiento2 = new DateTimeOffset?(_dato.FechaNacimiento);
                PaisSeleccioando = _paises.FirstOrDefault(pais => pais.PaisNombre == ConfiguracionGlobal.MensajeGlobal.Pais.ToString());
                _estadoSeleccionado = _estados.FirstOrDefault(estado => estado.EstadoNombre == ConfiguracionGlobal.MensajeGlobal.Estado.ToString());
            }
            else
            {
                
            }
            
        }
    

        public Empleado Dato
        {
            get => _dato;
            set
            {
                if (_dato != value)
                {
                    _dato = value;
                    Debug.WriteLine($"Valor de Dato cambiado. Nuevo valor: {value?.ToString() ?? "null"}");
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
                    Dato.Pais = _paisSeleccionado.PaisNombre.ToString();
                    OnPropertyChanged(nameof(Dato));
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

                    _estados = bd.GetEstadosPorPais(PaisSeleccioando.PaisID);
                    OnPropertyChanged(nameof(Estados));
                }

            }
        }
        public ObservableCollection<Estados> Estados
        {
            get { return _estados; }
            set
            {
                if (_estados != value)
                {
                    _estados = value;
                    OnPropertyChanged(nameof(Estados));
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


        //----------Commands------------------
        public ICommand RegresarCommand
        {
            get
            {
                return new RelayCommandNoParam(Regresar);
            }
        }
        private void Regresar()
        {
            ClassNavegacion.Regresar();
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(EditExecute, EditCanExecute);
            }
        }
        private void EditExecute(object empleado)
        {
            Debug.WriteLine(FechaNacimiento2.ToString() + "--------------------------------------------------");
            DateTime dateTime = FechaNacimiento2.HasValue ? FechaNacimiento2.Value.DateTime : DateTime.MaxValue;
            Dato.FechaNacimiento = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            Debug.WriteLine(Dato.FechaNacimiento.ToString() + "--------------------------------------------------");
            Dato.Pais = PaisSeleccioando.PaisNombre.ToString();
            Dato.Estado = EstadoSeleccionado.EstadoNombre.ToString();
            bd.Edit(Dato);
            Datos = bd.Get();
        }
        private bool EditCanExecute(object empleado)
        {
            return true;
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(DeleteExecute, DeleteCanExecute);
            }
        }
        private void DeleteExecute(object empleado)
        {
            bd.Delete(Dato);
            Datos = bd.Get();
            ClassNavegacion.Regresar();
        }
        private bool DeleteCanExecute(object empleado)
        {
            return true;
        }
    }
}
