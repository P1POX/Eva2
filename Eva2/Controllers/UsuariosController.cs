using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eva2.Models.Data;
using Eva2.Models.Entities;

namespace Eva2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos los usuarios
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/usuarios/{id}/prestamos
        [HttpGet("{id}/prestamos")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            // Verificar que el usuario exista
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            // Obtener los préstamos del usuario, incluyendo información del libro y devolución
            var historial = await _context.Prestamos
                .Where(p => p.UsuarioId == id)
                .Include(p => p.Libro)
                .Select(p => new
                {
                    PrestamoId = p.Id,
                    LibroId = p.LibroId,
                    TituloLibro = p.Libro!.Titulo,
                    FechaPrestamo = p.FechaPrestamo,
                    Devuelto = _context.Devoluciones.Any(d => d.PrestamoId == p.Id),
                    FechaDevolucion = _context.Devoluciones
                        .Where(d => d.PrestamoId == p.Id)
                        .Select(d => d.FechaDevolucion)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(historial);
        }

        /// <summary>
        /// Registrar un nuevo usuario 
        /// </summary>
        /// <param name="usuario">Datos del usuario.</param>
        /// <returns>El usuario creado.</returns>
        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> CrearUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }
    }
}