namespace SupermarketScanAPI.Entities.Products;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int IdPaisOrigen { get; set; }

    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public decimal? Peso { get; set; }

    public decimal? ValorEnergetico { get; set; }

    public decimal? Grasas { get; set; }

    public decimal? HidratosDeCarbono { get; set; }

    public decimal? Fibra { get; set; }

    public decimal? Proteinas { get; set; }

    public decimal? Sal { get; set; }

    public byte[]? Imagen { get; set; }

    public string? CodigoBarras { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual PaisOrigen IdPaisOrigenNavigation { get; set; } = null!;
}
