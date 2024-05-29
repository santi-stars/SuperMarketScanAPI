using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SmScan.API.AppDbContext.Usuarios;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmScan.DTO.Usuarios.Requests;

namespace SmScan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly string secretKey;
        private readonly UsuariosDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger;

        public AuthenticationController(IConfiguration config, UsuariosDbContext context, IMapper mapper, ILogger<UsuariosController> logger)
        {
            secretKey = config.GetSection("Settings").GetSection("SecretKey").ToString();
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("validar")]
        public async Task<ActionResult> Validar([FromBody] UsuarioLoginDto usuarioRequest)
        {
            _logger.LogInformation($"[POST] Usuario: {usuarioRequest.Email}");

            if (usuarioRequest == null)
            {
                _logger.LogInformation("[BadRequest] Usuario nulo");
                return BadRequest("Usuario nulo");
            }

            if (usuarioRequest.Email == null || usuarioRequest.Password == null)
            {
                _logger.LogInformation("[BadRequest] Email o contraseña nulos");
                return BadRequest("Email o contraseña nulos");
            }
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioRequest.Email);

            if (usuario == null)
            {
                _logger.LogInformation($"[NotFound] No existe el usuario con Email: {usuarioRequest.Email}");
                return NotFound();
            }

            if (usuarioRequest.Email == usuario.Email && usuarioRequest.Password == usuario.Contraseña)
            {
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuarioRequest.Email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                                                                SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                _logger.LogInformation($"[OK] Usuario: {usuarioRequest.Email}");
                return StatusCode(StatusCodes.Status200OK, new
                {
                    token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor))
                });
            }
            else
            {
                _logger.LogInformation("[Unauthorized] Usuario no autorizado");
                return StatusCode(StatusCodes.Status401Unauthorized, "Usuario no autorizado");
            }
        }
    }
}
