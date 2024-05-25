namespace SmScan.DTO.ProductosVista;
public class ProductosVistaNutricionalDto
{
    public string CodigoBarras { get; set; } = string.Empty;
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