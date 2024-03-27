﻿using SmScan.API.Domains.Productos;

namespace SmScan.API.Domains.Categorias;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Producto>? Productos { get; set; } = new List<Producto>();
}