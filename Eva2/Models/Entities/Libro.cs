using System.ComponentModel.DataAnnotations;

namespace Eva2.Models.Entities
{
    public class Libro
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "El título debe tener entre 1 y 150 caracteres.")]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El autor debe tener entre 1 y 100 caracteres.")]
        public required string Autor { get; set; }

        [Required(ErrorMessage = "El ISBN es obligatorio.")]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "El ISBN debe tener 10 o 13 dígitos numéricos.")]
        public required string ISBN { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de publicación")]
        public DateTime FechaPublicacion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser 0 o mayor.")]
        public int Cantidad { get; set; } = 0;
    }
}
