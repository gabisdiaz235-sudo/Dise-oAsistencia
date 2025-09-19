using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionAsistencia.Servicio_web
{
    internal class RegistroEntradaSalidaJson
    {
        [JsonProperty("Fecha")]
        public DateTime Fecha { get; set; }

        [JsonProperty("Hora")]
        public DateTime Hora { get; set; }

        [JsonProperty("Latitud")]
        public double Latitud { get; set; }

        [JsonProperty("Longitud")]
        public double Longitud { get; set; }
    }
}
