namespace SmScan.API.Domains.Productos;

public partial class ProductosVistaBase
{
    public string CodigoBarras { get; set; }

    public byte[] Imagen { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public decimal Precio { get; set; }

    public decimal? PrecioPorKg { get; set; }
}