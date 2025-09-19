using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaGestionAsistencia.Servicio_web
{
    public class HttpsClientHandlerService
    {
        public HttpClientHandler GetPlatformMessageHandler()
        {
            var handler = new HttpClientHandler();

            // Configurar el callback para omitir la validación del certificado
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                // Aquí puedes implementar tu lógica de validación personalizada
                // En este ejemplo, siempre se acepta el certificado sin importar los errores
                return true;
            };

            return handler;
        }
    }


}
