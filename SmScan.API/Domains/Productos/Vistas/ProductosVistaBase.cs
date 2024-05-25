namespace SmScan.API.Domains.Productos.Vistas;

public partial class ProductosVistaBase
{
    public string CodigoBarras { get; set; } = string.Empty;
    public byte[]? Imagen { get; set; } = new byte[0];
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; } = 0;
    public decimal? PrecioPorKg { get; set; }
}