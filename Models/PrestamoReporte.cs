using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class PrestamoReporte
    {
        public int PrestamoId { get; set; }
        public int SocioId { get; set; }

        [Display(Name = "Socio")]
        public string SocioNombre { get; set; } = string.Empty;

        [Display(Name = "DNI")]
        public string SocioDNI { get; set; } = string.Empty;

        public int LibroId { get; set; }

        [Display(Name = "Libro")]
        public string LibroTitulo { get; set; } = string.Empty;

        [Display(Name = "Fecha prestamo")]
        [DataType(DataType.Date)]
        public DateTime FechaPrestamo { get; set; }

        [Display(Name = "Fecha limite")]
        [DataType(DataType.Date)]
        public DateTime FechaLimite { get; set; }

        [Display(Name = "Fecha devolucion")]
        [DataType(DataType.Date)]
        public DateTime? FechaDevolucion { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; } = string.Empty;
    }
}