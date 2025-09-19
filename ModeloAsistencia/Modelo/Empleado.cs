using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloAsistencia.Modelo
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpleadoID { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }

        [StringLength(20)]
        public string ApellidoPaterno { get; set; }

        [StringLength(20)]
        public string ApellidoMaterno { get; set; }

        [StringLength(10)]
        public string Sexo { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(60)]
        public string Correo { get; set; }

        [Required]
        public string ContraseñaHash { get; set; }

        public int CodigoPostal { get; set; }

        [StringLength(30)]
        public string Calle { get; set; }

        public int NumeroExterior { get; set; }

        [StringLength(30)]
        public string Colonia { get; set; }

        [StringLength(30)]
        public string Municipio { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [StringLength(30)]
        public string Pais { get; set; }


    }
}
