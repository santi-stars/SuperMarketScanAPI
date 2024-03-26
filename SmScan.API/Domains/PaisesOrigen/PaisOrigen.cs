using SmScan.API.Domains.Productos;

namespace SmScan.API.Domains.PaisesOrigen;

public partial class PaisOrigen
{
    public int IdPaisOrigen { get; set; }

    public string NombrePais { get; set; } = null!;

    #region Relations
    public ICollection<Producto>? Productos { get; set; } = new List<Producto>();
    #endregion
}
