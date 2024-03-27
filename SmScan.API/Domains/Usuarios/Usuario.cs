namespace SmScan.API.Domains.Usuarios;

public partial class Usuario
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Contraseña { get; set; } = null!;
}
