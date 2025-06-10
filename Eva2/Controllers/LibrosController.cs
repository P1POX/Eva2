using Eva2.Models.Data;
using Eva2.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eva2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibrosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos los libros
        /// </summary>
        /// <returns>Lista de libros.</returns>
        // GET: api/libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetBooks()
        {
            return await _context.Libros.ToListAsync();
        }

        /// <summary>
        /// Muestra los detalles de un libro
        /// </summary>
        /// <param name="id">ID del libro.</param>
        /// <returns>El libro solicitado, si existe.</returns>
        // GET: api/libros/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetBook(Guid id)
        {
            var book = await _context.Libros.FindAsync(id);
            if (book == null)
                return NotFound();

            return book;
        }

        /// <summary>
        /// Crea un nuevo libro
        /// </summary>
        /// <param name="libro">Datos del libro.</param>
        /// <returns>El libro creado.</returns>
        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult<Libro>> CreateBook(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = libro.Id }, libro);
        }

        /// <summary>
        /// Edita la información de un libro
        /// </summary>
        /// <param name="id">ID del libro a actualizar.</param>
        /// <param name="libro">Datos actualizados del libro.</param>
        /// <returns>Sin contenido si se actualizó correctamente.</returns>
        // PUT: api/libros/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, Libro libro)
        {
            if (id != libro.Id)
                return BadRequest();

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Libros.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Elimina un libro
        /// </summary>
        /// <param name="id">ID del libro a eliminar.</param>
        /// <returns>Sin contenido si se eliminó correctamente, o error si está prestado.</returns>
        // DELETE: api/libros/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
                return NotFound("El libro no existe.");

            // Verificar si el libro tiene préstamos activos (sin devolución)
            bool libroPrestado = await _context.Prestamos
                .AnyAsync(p => p.LibroId == id &&
                               !_context.Devoluciones.Any(d => d.PrestamoId == p.Id));

            if (libroPrestado)
                return BadRequest("No se puede eliminar el libro porque tiene préstamos activos.");

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
