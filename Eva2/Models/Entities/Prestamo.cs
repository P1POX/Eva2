using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eva2.Models.Entities
{
    public class Prestamo
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Debe indicar un usuario válido.")]
        public Guid UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "Debe indicar un libro válido.")]
        public Guid LibroId { get; set; }

        [ForeignKey("LibroId")]
        public Libro? Libro { get; set; }

        public DateTime FechaPrestamo { get; set; } = DateTime.Now;
    }
}