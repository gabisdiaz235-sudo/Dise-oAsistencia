using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ModeloAsistencia.Modelo
{
    public class Estados
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstadoID { get; set; }

        [ForeignKey("Pais")]
        public int PaisID { get; set; }

        [StringLength(255)]
        public string EstadoNombre { get; set; }

        // Propiedad de navegación para la relación con la tabla Pais
        public Pais Pais { get; set; }
    }
}
