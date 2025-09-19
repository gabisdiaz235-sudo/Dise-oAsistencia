using ModeloAsistencia.Modelo;
using Newtonsoft.Json.Linq;
using SistemaGestionAsistencia.CarpetaSQL;
using SistemaGestionAsistencia.VistaModelo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SistemaGestionAsistencia.Servicio_web
{
    internal class InisioSesionSW : ViewModelBase
    {
        private readonly BD bd;
        private Empleado _dato;
        private DispatcherTimer _timer;
        private string _claveRecibida;

        public InisioSesionSW()
        {
            bd = new BD();
            _dato = new Empleado();
            
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += async (sender, e) => await EnviarYRecibirDatosAutomatico();
            _timer.Start();

        }


        public string ClaveRecibida
        {
            get { return _claveRecibida; }
            set
            {
                if (_claveRecibida != value)
                {
                    _claveRecibida = value;
                    OnPropertyChanged(nameof(ClaveRecibida));
                }
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
                    OnPropertyChanged(nameof(Dato));

                }
            }
        }


        public async void VerificarNuevosDatosDesdeWebService()
        {
            string url = "https://192.168.1.196:7066/My/Inicio-sesion-maui";
            try
            {
                using (HttpClient client = new HttpClient(new HttpsClientHandlerService().GetPlatformMessageHandler()))
                {

                    // Realizar la solicitud HTTP con el método HttpPost
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();


                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        try
                        {
                            var recibidos = JsonSerializer.Deserialize<Empleado>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                            // Procesar los datos recibidos en tu aplicación WinUI
                            Debug.WriteLine("Datos recibidos en WinUI: " + recibidos);

                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"Error de deserialización: {ex.Message}");
                        }

                    }
                    else
                    {
                        Debug.WriteLine("No hay datos por recibir");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones
                Debug.WriteLine($"Error al obtener datos desde el servicio web: {ex.Message}");
            }
        }


        private async Task EnviarYRecibirDatosAutomatico()
        {
            string clave = await EnviarDatosAlWebService();
            ClaveRecibida = clave;


            _ = ObtenerDatosDesdeWebService();

        }

        private async Task<string> EnviarDatosAlWebService()
        {
            string url = "https://192.168.1.196:7066/Inicio-sesion-maui";

            /*try
            {
                using (HttpClient client = new HttpClient())
                {
                    InicioSesion datos = new InicioSesion
                    {
                        Correo = Correo,
                        Contraseña = Contrasena
                    };

                    var response = await client.PostAsJsonAsync(url, datos);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        var result = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonResponse);
                        return result["Clave"];
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }*/

            return null;
        }

        private async Task<string> EnviarConfirmacionAlServidor(Empleado empleado)
        {
            string url = "https://192.168.1.196:7066/My/enviar-idNombre-a-maui";

            try
            {
                using (HttpClient client = new HttpClient(new HttpsClientHandlerService().GetPlatformMessageHandler()))
                {
                    var dataToSend = new
                    {
                        EmpleadoID = empleado.EmpleadoID,
                        Nombre = empleado.Nombre,
                        ClaveUnica = ConfiguracionGlobal.ClaveUnica
                    };
                    var content = new StringContent(JsonSerializer.Serialize(dataToSend), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        // Puedes procesar la respuesta del servidor según tus necesidades
                        Debug.WriteLine($"Respuesta del servidor: {jsonResponse}");
                        

                        // Retorna la respuesta del servidor
                        return jsonResponse;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }

            // Retorna una cadena vacía o un mensaje de error, según tu lógica
            return string.Empty;
        }


        private async Task ObtenerDatosDesdeWebService()
        {
            string url = "https://192.168.1.196:7066/My/Obtener-datos-inicio-sesion";

            try
            {
                using (HttpClient client = new HttpClient(new HttpsClientHandlerService().GetPlatformMessageHandler()))
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        var recibidos = JsonSerializer.Deserialize<Empleado>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        var clave = JsonSerializer.Deserialize<ClaveSW>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        ConfiguracionGlobal.ClaveUnica = clave.ClaveUnica;
                        //Obtener información adicional del empleado
                        Empleado informacionEmpleado = bd.ObtenerInformacionEmpleado(recibidos.Correo, recibidos.ContraseñaHash);

                        // Procesar la información del empleado en tu ViewModel
                        if (informacionEmpleado != null)
                        {
                            Debug.WriteLine($"Empleado encontrado: ID={informacionEmpleado.EmpleadoID}, Nombre={informacionEmpleado.Nombre}");

                            // Puedes asignar los datos a tus propiedades, etc.
                            _ = EnviarConfirmacionAlServidor(informacionEmpleado);
                        }
                        else
                        {
                            Debug.WriteLine("Credenciales no válidas");
                        }

                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }
        }


    }
}
