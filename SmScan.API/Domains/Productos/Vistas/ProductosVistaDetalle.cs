namespace SmScan.API.Domains.Productos;

public partial class ProductosVistaDetalle
{
    public string CodigoBarras { get; set; }

    public byte[] Imagen { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public decimal Precio { get; set; }

    public decimal? PrecioPorKg { get; set; }

    public decimal? ValorEnergetico { get; set; }

    public decimal? Grasas { get; set; }

    public decimal? HidratosDeCarbono { get; set; }

    public decimal? Fibra { get; set; }

    public decimal? Proteinas { get; set; }

    public decimal? Sal { get; set; }

    public string NombreCategoria { get; set; }

    public string NombrePais { get; set; }
}