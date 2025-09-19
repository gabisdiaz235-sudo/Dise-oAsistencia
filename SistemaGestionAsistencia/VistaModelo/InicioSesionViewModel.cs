using ModeloAsistencia.Modelo;
using SistemaGestionAsistencia.CarpetaSQL;
using SistemaGestionAsistencia.Commands;
using SistemaGestionAsistencia.Vistas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace SistemaGestionAsistencia.VistaModelo
{
    class InicioSesionViewModel : ViewModelBase
    {
        private readonly BD bd;
        private ObservableCollection<Administrador> _datos;
        private Administrador _dato;
        private ICommand _inicioSesionCommand;
        private DispatcherTimer _mensajeTimer;

        public InicioSesionViewModel()
        {
            bd = new BD();
            _dato = new Administrador();
            //_datos = bd.Get();
            _mensajeTimer = new DispatcherTimer();
            _mensajeTimer.Tick += MensajeTimer_Tick;
        }


        private void MensajeTimer_Tick(object sender, EventArgs e)
        {
            Mensaje = ""; // Limpiar el mensaje después de cierto tiempo
            _mensajeTimer.Stop(); // Detener el temporizador
        }

        public Administrador Dato
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
        public ObservableCollection<Administrador> Datos
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


        public ICommand IniciSesionCommand
        {
            get
            {
                return _inicioSesionCommand ?? (_inicioSesionCommand = new RelayCommand(InicioSesionExecute, InicioSesionCanExecute));
            }
        }
        private void InicioSesionExecute(object administrador)
        {
            if (CamposNulos(Dato))
            {
                if (bd.ValidarCredenciales(Dato.CorreoAdmin, Dato.ContraseñaHashAdmin))
                {
                     
                    OnInicioSesionExitoso.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Mensaje = "No existe un administrador con ese correo";
                    _mensajeTimer.Interval = TimeSpan.FromSeconds(5);
                    _mensajeTimer.Start();
                }
            }
            
        }
        private bool InicioSesionCanExecute(object administrador)
        {
            return true;
        }

        public event EventHandler OnInicioSesionExitoso;



        private bool CamposNulos(Administrador admin)
        {
            // Verificar si algún campo específico es nulo
            if (admin.CorreoAdmin == null && admin.ContraseñaHashAdmin == null)
            {
                Mensaje = "Campos vacios";
                _mensajeTimer.Interval = TimeSpan.FromSeconds(5);
                _mensajeTimer.Start();
                return false;
            }
            return true;
        }


        private string _mensaje;
        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                _mensaje = value;
                OnPropertyChanged(nameof(Mensaje));
            }
        }
    }
}
