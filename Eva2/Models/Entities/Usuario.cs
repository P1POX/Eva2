using System.ComponentModel.DataAnnotations;

namespace Eva2.Models.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El RUT es obligatorio.")]
        [RegularExpression(@"^\d{1,2}\.?\d{3}\.?\d{3}-?[\dkK]$", ErrorMessage = "El RUT no tiene un formato válido.")]
        public required string Rut { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres.")]
        public required string Apellido { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe tener exactamente 9 dígitos.")]
        public required string Telefono { get; set; }
    }
}
