using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using ModeloAsistencia.Modelo;
using SistemaGestionAsistencia.CarpetaSQL;
using SistemaGestionAsistencia.VistaModelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System;

namespace SistemaGestionAsistencia.Servicio_web
{
    class RecibirDatosSW : ViewModelBase
    {
        private readonly BD bd;
        private RegistroEntradaSalida _dato;
        private readonly Microsoft.UI.Dispatching.DispatcherQueue _dispatcherQueue;

        public RecibirDatosSW(Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue)
        {
            bd = new BD();
            _dato = new RegistroEntradaSalida();
            _dispatcherQueue = dispatcherQueue ?? throw new ArgumentNullException(nameof(dispatcherQueue));

            // Iniciar el proceso para verificar nuevos datos cada 5 segundos
            VerificarNuevosDatosCada5SegundosAsync();
        }

        public RegistroEntradaSalida Dato
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

        private async void VerificarNuevosDatosCada5SegundosAsync()
        {
            while (true)
            {
                await VerificarNuevosDatosDesdeWebServiceAsync();

                // Esperar 5 segundos antes de la próxima ejecución
                await Task.Delay(5000);
            }
        }

        public async Task VerificarNuevosDatosDesdeWebServiceAsync()
        {
            string url = "https://192.168.1.196:7066/My/enviar-datos-a-winui";
            try
            {
                using (HttpClient client = new HttpClient(new HttpsClientHandlerService().GetPlatformMessageHandler()))
                {
                    // Realizar la solicitud HTTP con el método HttpGet
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        try
                        {
                            var recibidos = JsonSerializer.Deserialize<RegistroEntradaSalida>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                            // Procesar los datos recibidos en tu aplicación WinUI
                            Debug.WriteLine("Datos recibidos en WinUI: " + recibidos);
                            bd.AddR(recibidos);
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
    }

}
