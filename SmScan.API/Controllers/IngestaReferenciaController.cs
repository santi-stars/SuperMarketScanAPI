using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmScan.API.Domains.IngestaReferencia;
using SmScan.API.AppDbContext.Productos;

namespace SmScan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngestaReferenciaController : ControllerBase
    {
        private readonly ProductosDbContext _context;
        private readonly ILogger<IngestaReferenciaController> _logger;

        public IngestaReferenciaController(ProductosDbContext context, ILogger<IngestaReferenciaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngestaReferencia>>> GetIngestaReferencia()
        {
            _logger.LogInformation("GET IngestaReferencia");

            return await _context.IngestaReferencia.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IngestaReferencia>> PostIngestaReferencia(IngestaReferencia ingestaRef)
        {
            _context.IngestaReferencia.Add(ingestaRef);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngestaReferencia", new { id = ingestaRef.IdIngestaRef }, ingestaRef);
        }

        [HttpPut("{idIngestaRef:int}")]
        public async Task<IActionResult> PutIngestaReferencia(int idIngestaRef, [FromBody] IngestaReferencia ingestaReferencia)
        {
            if (idIngestaRef != ingestaReferencia.IdIngestaRef) return BadRequest("El ID no coincide");

            _context.Entry(ingestaReferencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.IngestaReferencia.Any(e => e.IdIngestaRef == idIngestaRef)) return NotFound();

                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{idIngestaRef:int}")]
        public async Task<IActionResult> DeleteIngestaReferencia(int idIngestaRef)
        {
            var ingestaRef = await _context.IngestaReferencia.FindAsync(idIngestaRef);
            if (ingestaRef == null) return NotFound();

            _context.IngestaReferencia.Remove(ingestaRef);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
