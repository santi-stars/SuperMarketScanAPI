using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmScan.API.Domains.Categorias;
using SmScan.API.AppDbContext.Productos;
using Microsoft.AspNetCore.Authorization;

namespace SmScan.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ProductosDbContext _context;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ProductosDbContext context, ILogger<CategoriasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
        {
            _logger.LogInformation("GET Categorias");

            return await _context.Categorias.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.IdCategoria }, categoria);
        }

        [HttpPut("{idCategoria:int}")]
        public async Task<IActionResult> PutCategoria(int idCategoria, Categoria categoria)
        {
            if (idCategoria != categoria.IdCategoria) return BadRequest("El ID no coincide");

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Categorias.Any(e => e.IdCategoria == idCategoria)) return NotFound();

                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{idCategoria:int}")]
        public async Task<IActionResult> DeleteCategoria(int idCategoria)
        {
            var categoria = await _context.Categorias.FindAsync(idCategoria);
            if (categoria == null) return NotFound();

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
