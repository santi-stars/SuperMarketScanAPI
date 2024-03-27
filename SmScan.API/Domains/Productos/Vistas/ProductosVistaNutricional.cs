namespace SmScan.API.Domains.Productos.Vistas;

public partial class ProductosVistaNutricional
{
    public string CodigoBarras { get; set; }
    public decimal? ValorEnergetico { get; set; }
    public decimal? PorcentajeValorEnergetico { get; set; }
    public decimal? Grasas { get; set; }
    public decimal? PorcentajeGrasas { get; set; }
    public decimal? HidratosDeCarbono { get; set; }
    public decimal? PorcentajeHidratosDeCarbono { get; set; }
    public decimal? Fibra { get; set; }
    public decimal? PorcentajeFibra { get; set; }
    public decimal? Proteinas { get; set; }
    public decimal? PorcentajeProteinas { get; set; }
    public decimal? Sal { get; set; }
    public decimal? PorcentajeSal { get; set; }
}