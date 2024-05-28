namespace SmScan.DTO.ProductosVista;
public class ProductosVistaBaseDto
{
    public string CodigoBarras { get; set; } = string.Empty;
    public string? Imagen { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; } = 0;
    public decimal? PrecioPorKg { get; set; }
}