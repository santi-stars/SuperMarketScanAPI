namespace SmScan.DTO.ProductosVista
{
    public class ProductosVistaDetalleDto
    {
        public string CodigoBarras { get; set; } = string.Empty;
        public byte[] Imagen { get; set; } = new byte[0];
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal? PrecioPorKg { get; set; }
        public decimal? ValorEnergetico { get; set; }
        public decimal? Grasas { get; set; }
        public decimal? HidratosDeCarbono { get; set; }
        public decimal? Fibra { get; set; }
        public decimal? Proteinas { get; set; }
        public decimal? Sal { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public string NombrePais { get; set; } = string.Empty;
    }
}
