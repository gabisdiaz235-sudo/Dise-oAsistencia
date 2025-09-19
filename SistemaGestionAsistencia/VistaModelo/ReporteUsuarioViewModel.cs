using ModeloAsistencia.Modelo;
using SistemaGestionAsistencia.CarpetaSQL;
using SistemaGestionAsistencia.Commands;
using SistemaGestionAsistencia.Navegacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SistemaGestionAsistencia.VistaModelo
{
    internal class ReporteUsuarioViewModel : ViewModelBase
    {
        private readonly BD bd;
        private ObservableCollection<Empleado> _datos;
        private ObservableCollection<RegistroEntradaSalida> _datosES;
        private Empleado _dato;
        private DateTimeOffset? _fechaNacimiento2;

        public ReporteUsuarioViewModel()
        {
            bd = new BD();
            _dato = new Empleado();
            _datos = bd.Get();


            _dato = ConfiguracionGlobal.MensajeGlobal;
            if (_dato != null)
            {
                _fechaNacimiento2 = new DateTimeOffset?(_dato.FechaNacimiento);
            }
            else
            {

            }
            _datosES = bd.GetES(_dato.EmpleadoID);
            
        }
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
        public ObservableCollection<RegistroEntradaSalida> DatosES
        {
            get => _datosES;
            set
            {
                if (_datosES != value)
                {
                    _datosES = value;
                    OnPropertyChanged(nameof(DatosES));

                }
            }
        }
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
    }
}
