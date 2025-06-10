using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eva2.Models.Entities
{
    public class Devolucion
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Debe indicar el préstamo asociado.")]
        public Guid PrestamoId { get; set; }

        [ForeignKey("PrestamoId")]
        public Prestamo? Prestamo { get; set; }

        public DateTime FechaDevolucion { get; set; } = DateTime.Now;
    }
}
