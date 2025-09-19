using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModeloAsistencia.Modelo
{
    public class RegistroEntradaSalida
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EntradaSalidaID { get; set; }

        [ForeignKey("Empleado")]
        public int EmpleadoID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime HoraEntrada { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime HoraSalida { get; set; }

        public double Latitud { get; set; }

        public double Longitud { get; set; }
    }
}
