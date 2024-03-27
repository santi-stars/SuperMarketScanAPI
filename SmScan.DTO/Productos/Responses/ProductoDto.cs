using SmScan.DTO.Categorias.Responses;
using SmScan.DTO.IngestasReferencia.Responses;
using SmScan.DTO.PaisesOrigen.Responses;

namespace SmScan.DTO.Productos.Responses
{
    public class ProductoDto
    {
        public string Nombre { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Imagen { get; set; } = null!;
        public decimal ValorEnergetico { get; set; }
        public decimal Grasas { get; set; }
        public decimal HidratosDeCarbono { get; set; }
        public decimal Fibra { get; set; }
        public decimal Proteinas { get; set; }
        public decimal Sal { get; set; }
        public IngestaReferenciaDto? IngestaReferencia { get; set; } = null;
        public PaisOrigenDto PaisOrigen { get; set; } = null!;
        public CategoriaDto Categoria { get; set; } = null!;
    }
}
