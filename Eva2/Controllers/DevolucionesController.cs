using Eva2.Models.Data;
using Eva2.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eva2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DevolucionesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registrar la devolución de un libro
        /// </summary>
        /// <param name="devolucion">Datos de la devolución.</param>
        /// <returns>Devolución creada.</returns>
        // POST: api/devoluciones
        [HttpPost]
        public async Task<ActionResult<Devolucion>> CrearDevolucion([FromBody] Devolucion devolucion)
        {
            // Validar que el préstamo existe
            var prestamo = await _context.Prestamos.FindAsync(devolucion.PrestamoId);
            if (prestamo == null)
                return NotFound("El préstamo no existe.");

            // Validar que no haya una devolución registrada para ese préstamo
            var devolucionExistente = await _context.Devoluciones
                .AnyAsync(d => d.PrestamoId == devolucion.PrestamoId);

            if (devolucionExistente)
                return BadRequest("Ya existe una devolución registrada para este préstamo.");

            // Registrar la devolución
            devolucion.Id = Guid.NewGuid();
            devolucion.FechaDevolucion = DateTime.Now;

            _context.Devoluciones.Add(devolucion);
            await _context.SaveChangesAsync();

            return Ok(devolucion);
        }
    }
}