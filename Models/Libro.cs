using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class Libro
    {
        public int LibroId { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio")]
        [StringLength(150, ErrorMessage = "El titulo no puede superar los 150 caracteres")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ISBN es obligatorio")]
        [StringLength(20, ErrorMessage = "El ISBN no puede superar los 20 caracteres")]
        [DataType(DataType.Text)]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un autor")]
        [Display(Name = "Autor")]
        public int AutorId { get; set; }

        // Solo para mostrar el nombre del autor en las vistas (viene del INNER JOIN)
        [Display(Name = "Autor")]
        public string? AutorNombre { get; set; }

        [Required(ErrorMessage = "La cantidad de ejemplares es obligatoria")]
        [Range(0, 1000, ErrorMessage = "Los ejemplares deben estar entre 0 y 1000")]
        [Display(Name = "Ejemplares")]
        public int Ejemplares { get; set; }

        public bool Activo { get; set; } = true;
    }
}