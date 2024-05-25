using Microsoft.EntityFrameworkCore;
using SmScan.API.Domains.Categorias;
using SmScan.API.Domains.IngestaReferencia;
using SmScan.API.Domains.PaisesOrigen;
using SmScan.API.Domains.Productos;
using SmScan.API.Domains.Productos.Vistas;

namespace SmScan.API.AppDbContext.Productos;

public partial class ProductosDbContext : DbContext
{
    public ProductosDbContext()
    {
    }

    public ProductosDbContext(DbContextOptions<ProductosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<IngestaReferencia> IngestaReferencia { get; set; }
    public virtual DbSet<PaisOrigen> PaisOrigen { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<ProductosVistaBase> ProductosVistaBase { get; set; }
    public virtual DbSet<ProductosVistaDetalle> ProductosVistaDetalle { get; set; }
    public virtual DbSet<ProductosVistaNutricional> ProductosVistaNutricional { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DBConfiguration__SupermarketScan_Products__cn"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10A98E5AAF");

            entity.Property(e => e.NombreCategoria).HasMaxLength(100);
        });

        modelBuilder.Entity<IngestaReferencia>(entity =>
        {
            entity.HasKey(e => e.IdIngestaRef).HasName("PK__IngestaR__8A694C36C7CADB33");

            entity.ToTable(tb => tb.HasTrigger("trg_onlyOneRow"));

            entity.Property(e => e.FibraRef).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GrasasRef).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HidratosDeCarbonoRef).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProteinasRef).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SalRef).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorEnergeticoRef).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PaisOrigen>(entity =>
        {
            entity.HasKey(e => e.IdPaisOrigen).HasName("PK__PaisOrig__3712CD21B8989244");

            entity.ToTable("PaisOrigen");

            entity.Property(e => e.NombrePais).HasMaxLength(100);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210927295B0");

            entity.HasIndex(e => e.CodigoBarras).IsUnique();

            entity.Property(e => e.CodigoBarras).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Fibra).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Grasas).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HidratosDeCarbono).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Peso).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Proteinas).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorEnergetico).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Productos__IdCat__44FF419A");

            entity.HasOne(d => d.PaisOrigen).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdPaisOrigen)
                .HasConstraintName("FK__Productos__IdPai__440B1D61");
        });

        modelBuilder.Entity<ProductosVistaBase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductosVistaBase");

            entity.Property(e => e.CodigoBarras).HasMaxLength(50);
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(271);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioPorKg).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ProductosVistaDetalle>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductosVistaDetalle");

            entity.Property(e => e.CodigoBarras).HasMaxLength(50);
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(271);
            entity.Property(e => e.Fibra).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Grasas).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HidratosDeCarbono).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.NombreCategoria)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.NombrePais)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioPorKg).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Proteinas).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorEnergetico).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ProductosVistaNutricional>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductosVistaNutricional");

            entity.Property(e => e.CodigoBarras).HasMaxLength(50);
            entity.Property(e => e.Fibra).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Grasas).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HidratosDeCarbono).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PorcentajeFibra).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.PorcentajeGrasas).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.PorcentajeHidratosDeCarbono).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.PorcentajeProteinas).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.PorcentajeSal).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.PorcentajeValorEnergetico).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.Proteinas).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorEnergetico).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
