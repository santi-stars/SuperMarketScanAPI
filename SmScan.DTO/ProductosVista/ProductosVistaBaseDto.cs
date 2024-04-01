namespace SmScan.DTO.ProductosVista;
public class ProductosVistaBaseDto
{
    public string CodigoBarras { get; set; } = string.Empty;
    public byte[] Imagen { get; set; } = new byte[0];
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public decimal? PrecioPorKg { get; set; }
}