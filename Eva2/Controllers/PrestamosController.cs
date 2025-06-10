using Eva2.Models.Data;
using Eva2.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eva2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrestamosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registrar un nuevo préstamo 
        /// </summary>
        /// <param name="prestamo">Datos del préstamo.</param>
        /// <returns>El préstamo creado.</returns>
        // POST: api/prestamos
        [HttpPost]
        public async Task<ActionResult<Prestamo>> CrearPrestamo(Prestamo prestamo)
        {
            // Validar que el libro existe
            var libro = await _context.Libros.FindAsync(prestamo.LibroId);
            if (libro == null)
                return NotFound("El libro no existe.");

            // Validar que el usuario existe
            var usuario = await _context.Usuarios.FindAsync(prestamo.UsuarioId);
            if (usuario == null)
                return NotFound("El usuario no existe.");

            // Obtener la cantidad de préstamos activos (sin devolución) del libro
            var prestamosActivos = await _context.Prestamos
                .Where(p => p.LibroId == prestamo.LibroId &&
                            !_context.Devoluciones.Any(d => d.PrestamoId == p.Id))
                .CountAsync();

            // Calcular el stock disponible
            int stockDisponible = libro.Cantidad - prestamosActivos;

            if (stockDisponible <= 0)
                return BadRequest("No hay stock disponible para este libro.");

            // Registrar el préstamo
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();

            return Ok(prestamo);
        }
    }
}