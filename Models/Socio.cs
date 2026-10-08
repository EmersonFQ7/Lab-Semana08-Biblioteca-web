using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models
{
    public class Socio
    {
        public int SocioId { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI debe tener exactamente 8 digitos")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe contener solo numeros")]
        [Display(Name = "DNI")]
        public string DNI { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(100, ErrorMessage = "El email no puede superar los 100 caracteres")]
        [EmailAddress(ErrorMessage = "Debe ser un email valido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}