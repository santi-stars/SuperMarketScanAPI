using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmScanAPI.AppDbContext.Usuarios;
using SmScanAPI.Domains.Usuarios;

namespace SmScanAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosDbContext _context;

        public UsuariosController(UsuariosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{idUsuario:int}")]
        public async Task<IActionResult> PutUsuario(int idUsuario, Usuario usuario)
        {
            if (idUsuario != usuario.IdUsuario) return BadRequest("El ID no coincide");

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.IdUsuario == idUsuario)) return NotFound();

                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{idUsuario:int}")]
        public async Task<IActionResult> DeleteUsuario(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
