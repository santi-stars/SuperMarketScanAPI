using SmScan.DTO.Categorias;
using SmScan.DTO.IngestaReferencia;
using SmScan.DTO.PaisesOrigen;

namespace SmScan.DTO.Productos;
public class ProductoRequestDto
{
    public string Nombre { get; set; } = null!;
    public string Marca { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public decimal Precio { get; set; } = 0;
    public decimal Peso { get; set; }
    public decimal ValorEnergetico { get; set; }
    public decimal Grasas { get; set; }
    public decimal HidratosDeCarbono { get; set; }
    public decimal Fibra { get; set; }
    public decimal Proteinas { get; set; }
    public decimal Sal { get; set; }
    public string? Imagen { get; set; }
    public string CodigoBarras { get; set; } = null!;

    public IngestaRefResponseDto? IngestaReferencia { get; set; } = null;
    public PaisOrigenResponseDto PaisOrigen { get; set; } = null!;
    public CategoriaResponseDto Categoria { get; set; } = null!;
}

