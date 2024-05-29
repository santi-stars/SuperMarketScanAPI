using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmScan.API.Migrations.ProductosDb
{
    /// <inheritdoc />
    public partial class productos_inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCategoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__A3C02A10A98E5AAF", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "IngestaReferencia",
                columns: table => new
                {
                    IdIngestaRef = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorEnergeticoRef = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    GrasasRef = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    HidratosDeCarbonoRef = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FibraRef = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProteinasRef = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SalRef = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__IngestaR__8A694C36C7CADB33", x => x.IdIngestaRef);
                });

            migrationBuilder.CreateTable(
                name: "PaisOrigen",
                columns: table => new
                {
                    IdPaisOrigen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePais = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaisOrig__3712CD21B8989244", x => x.IdPaisOrigen);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaisOrigen = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ValorEnergetico = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Grasas = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    HidratosDeCarbono = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Fibra = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Proteinas = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Sal = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoBarras = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__09889210927295B0", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK__Productos__IdCat__44FF419A",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Productos__IdPai__440B1D61",
                        column: x => x.IdPaisOrigen,
                        principalTable: "PaisOrigen",
                        principalColumn: "IdPaisOrigen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CodigoBarras",
                table: "Productos",
                column: "CodigoBarras",
                unique: true,
                filter: "[CodigoBarras] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdCategoria",
                table: "Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdPaisOrigen",
                table: "Productos",
                column: "IdPaisOrigen");

            // Create the function
            migrationBuilder.Sql(@"
                CREATE FUNCTION dbo.GetIngestaReferencia ()
                RETURNS @IngestaRef TABLE
                (
                    ValorEnergeticoRef DECIMAL(10, 2),
                    GrasasRef DECIMAL(10, 2),
                    HidratosDeCarbonoRef DECIMAL(10, 2),
                    FibraRef DECIMAL(10, 2),
                    ProteinasRef DECIMAL(10, 2),
                    SalRef DECIMAL(10, 2)
                )
                AS
                BEGIN
                    INSERT INTO @IngestaRef
                    SELECT TOP 1
                        ValorEnergeticoRef,
                        GrasasRef,
                        HidratosDeCarbonoRef,
                        FibraRef,
                        ProteinasRef,
                        SalRef
                    FROM IngestaReferencia
                    RETURN
                END
                ");

            // Create the views
            migrationBuilder.Sql(@"
                CREATE VIEW 
                    ProductosVistaDetalle AS
                SELECT 
                    pro.CodigoBarras,
                    pro.Imagen, 
                    pro.Nombre, 
                    CONCAT(CAST(pro.Peso AS NVARCHAR(10)), ' Kg - ', pro.Descripcion) AS Descripcion,
                    pro.Precio, 
                    CAST(pro.Precio/pro.Peso AS DECIMAL(10, 2)) AS PrecioPorKg,
                    pro.ValorEnergetico, 
                    pro.Grasas, 
                    pro.HidratosDeCarbono, 
                    pro.Fibra, 
                    pro.Proteinas, 
                    pro.Sal,
                    c.NombreCategoria,
                    p.NombrePais
                FROM Productos pro
                    INNER JOIN Categorias c ON c.IdCategoria = pro.IdCategoria
                    INNER JOIN PaisOrigen p ON p.IdPaisOrigen = pro.IdPaisOrigen;
                ");

            migrationBuilder.Sql(@"
                CREATE VIEW 
                    ProductosVistaBase AS
                SELECT 
                    pro.CodigoBarras,
                    pro.Imagen, 
                    pro.Nombre, 
                    CONCAT(CAST(pro.Peso AS NVARCHAR(10)), ' Kg - ', pro.Descripcion) AS Descripcion, 
                    pro.Precio, 
                    CAST(pro.Precio/pro.Peso AS DECIMAL(10, 2)) AS PrecioPorKg
                FROM Productos pro;
                ");

            migrationBuilder.Sql(@"
                CREATE VIEW 
                    ProductosVistaNutricional AS
                SELECT 
                    pro.CodigoBarras,
                    pro.ValorEnergetico, 
                    CAST((pro.ValorEnergetico / ref.ValorEnergeticoRef) * 100 AS DECIMAL(10, 1)) AS PorcentajeValorEnergetico,
                    pro.Grasas, 
                    CAST((pro.Grasas / ref.GrasasRef) * 100 AS DECIMAL(10, 1)) AS PorcentajeGrasas,
                    pro.HidratosDeCarbono, 
                    CAST((pro.HidratosDeCarbono / ref.HidratosDeCarbonoRef) * 100 AS DECIMAL(10, 1)) AS PorcentajeHidratosDeCarbono,
                    pro.Fibra, 
                    CAST((pro.Fibra / ref.FibraRef) * 100 AS DECIMAL(10, 1)) AS PorcentajeFibra,
                    pro.Proteinas, 
                    CAST((pro.Proteinas / ref.ProteinasRef) * 100 AS DECIMAL(10, 1)) AS PorcentajeProteinas,
                    pro.Sal,
                    CAST((pro.Sal / ref.SalRef) * 100 AS DECIMAL(10, 1)) AS PorcentajeSal
                FROM Productos pro
                CROSS APPLY dbo.GetIngestaReferencia() ref
                ");

            // Create the trigger
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_onlyOneRow
                ON IngestaReferencia
                AFTER INSERT
                AS
                BEGIN
                    IF (SELECT COUNT(*) FROM IngestaReferencia) > 1
                    BEGIN
                        RAISERROR ('No se puede insertar más de un registro en la tabla IngestaReferencia.', 16, 1);
                        ROLLBACK TRANSACTION;
                    END
                END;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngestaReferencia");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "PaisOrigen");

            // Drop the views
            migrationBuilder.Sql("DROP VIEW IF EXISTS ProductosVistaDetalle;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS ProductosVistaBase;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS ProductosVistaNutricional;");

            // Drop the function
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.GetIngestaReferencia;");

            // Drop the trigger
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_onlyOneRow;");
        }
    }
}
