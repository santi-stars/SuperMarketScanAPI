using SmScan.API.Domains.Productos;
using SmScan.API.Validations;

namespace SmScan.API.Domains.Categorias;

public partial class Categoria
{
    public int IdCategoria { get; set; }
    [PrimeraLetraMayuscula]
    public string NombreCategoria { get; set; } = null!;

    #region Relations
    public virtual ICollection<Producto>? Productos { get; set; }
    #endregion
}
