using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmScan.API.AppDbContext.Productos;
using SmScan.API.Domains.Productos;
using SmScan.DTO.ProductosVista;

namespace SmScan.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(ProductosDbContext context, IMapper mapper, ILogger<ProductosController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            _logger.LogInformation("GET Productos");

            return await _context.Productos.ToListAsync();
        }

        [HttpGet]
        [Route("base")]
        public async Task<ActionResult<IEnumerable<ProductosVistaBaseDto>>> GetProductosVistaBase(string? codigoBarras)
        {

            if (!string.IsNullOrEmpty(codigoBarras))
            {
                _logger.LogInformation($"GET Productos vista BASE con codigo de barras: {codigoBarras}");
                var productoVistaBase = await _context.ProductosVistaBase.FirstOrDefaultAsync(p => p.CodigoBarras == codigoBarras);
                if (productoVistaBase == null) return NotFound();
                else return Ok(_mapper.Map<ProductosVistaBaseDto>(productoVistaBase));
            }
            else
            {
                _logger.LogInformation("GET Productos vista BASE");
                var productosVistaBase = await _context.ProductosVistaBase.ToListAsync();
                return Ok(_mapper.Map<List<ProductosVistaBaseDto>>(productosVistaBase));
            }
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.IdProducto }, producto);
        }

        [HttpPut("{idProducto:int}")]
        public async Task<IActionResult> PutProducto(int idProducto, Producto producto)
        {
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
