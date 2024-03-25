namespace SupermarketScanAPI.Entities.Products;

public partial class PaisOrigen
{
    public int IdPaisOrigen { get; set; }

    public string NombrePais { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
