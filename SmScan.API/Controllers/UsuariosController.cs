using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmScan.API.AppDbContext.Usuarios;
using SmScan.API.Domains.Usuarios;
using SmScan.DTO.Usuarios;

namespace SmScan.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(UsuariosDbContext context, IMapper mapper, ILogger<UsuariosController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetUsuario()
        {
            _logger.LogInformation("[GET] Usuarios");

            var usuarios = await _context.Usuarios.ToListAsync();

            return _mapper.Map<List<UsuarioResponseDto>>(usuarios);
        }

        [HttpGet("id:int")]
        public async Task<ActionResult<UsuarioResponseDto>> GetUsuarioById(int id)
        {
            _logger.LogInformation($"[GET] Usuario por ID: {id}");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                _logger.LogInformation($"[NotFound] Usuario con ID: {id}");

                return NotFound();
            }

            return _mapper.Map<UsuarioResponseDto>(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioRequestDto usuarioDto)
        {
            _logger.LogInformation($"[POST] Usuario: {usuarioDto.Email}");

            var existeEmail = await _context.Usuarios.AnyAsync(u => u.Email == usuarioDto.Email);

            if (existeEmail)
            {
                _logger.LogInformation($"[BadRequest] Usuario: {usuarioDto.Email}");

                return BadRequest($"El email {usuarioDto.Email} ya existe");
            }

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuarioDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioRequestDto usuarioDto)
        {
            _logger.LogInformation($"[PUT] Usuario: {usuarioDto.Email}");

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.IdUsuario == id))
                {
                    _logger.LogInformation($"[NotFound] Usuario con ID: {id}");

                    return NotFound();
                }
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
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
