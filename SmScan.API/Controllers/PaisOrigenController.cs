using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmScan.API.AppDbContext.Productos;
using SmScan.API.Domains.PaisesOrigen;

namespace SmScanAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisOrigenController : ControllerBase
    {
        private readonly ProductosDbContext _context;

        public PaisOrigenController(ProductosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaisOrigen>>> GetPaisOrigen()
        {
            return await _context.PaisOrigen.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PaisOrigen>> PostPaisOrigen(PaisOrigen paisorigen)
        {
            _context.PaisOrigen.Add(paisorigen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaisOrigen", new { id = paisorigen.IdPaisOrigen }, paisorigen);
        }

        [HttpPut("{idPaisOrigen:int}")]
        public async Task<IActionResult> PutPaisOrigen(int idPaisOrigen, PaisOrigen paisorigen)
        {
            if (idPaisOrigen != paisorigen.IdPaisOrigen) return BadRequest("El ID no coincide");

            _context.Entry(paisorigen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PaisOrigen.Any(e => e.IdPaisOrigen == idPaisOrigen)) return NotFound();

                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{idPaisOrigen:int}")]
        public async Task<IActionResult> DeletePaisOrigen(int idPaisOrigen)
        {
            var paisorigen = await _context.PaisOrigen.FindAsync(idPaisOrigen);
            if (paisorigen == null) return NotFound();

            _context.PaisOrigen.Remove(paisorigen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
