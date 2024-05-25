namespace SmScan.API.Domains.Productos.Vistas;

public partial class ProductosVistaBase
{
    public string CodigoBarras { get; set; } = string.Empty;
    public string? Imagen { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; } = 0;
    public decimal? PrecioPorKg { get; set; }
}