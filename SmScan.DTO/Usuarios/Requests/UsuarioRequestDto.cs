﻿namespace SmScan.DTO.Usuarios;
public class UsuarioRequestDto
{
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Contraseña { get; set; } = null!;
}

