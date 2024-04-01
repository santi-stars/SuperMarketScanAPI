using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmScan.API.AppDbContext.Productos;
using SmScan.API.Domains.Productos;

namespace SmScan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosDbContext _context;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(ProductosDbContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        {
            _logger.LogInformation("GET Productos");

            return await _context.Productos.ToListAsync();
        }

        // TODO: 3 gets de ProductosVista...

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            //TODO: Validar que el producto no exista
            //TODO: Validar que la categoria y el pais de origen existan
            //TODO: validarlo en todos los controllers
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.IdProducto }, producto);
        }

        [HttpPut("{idProducto:int}")]
        public async Task<IActionResult> PutProducto(int idProducto, Producto producto)
        {
            //TODO: Validar que la categoria y el pais de origen existan

            if (idProducto != producto.IdProducto) return BadRequest("El ID no coincide");

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.IdProducto == idProducto)) return NotFound();

                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{idProducto:int}")]
        public async Task<IActionResult> DeleteProducto(int idProducto)
        {
            var producto = await _context.Productos.FindAsync(idProducto);
            if (producto == null) return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
