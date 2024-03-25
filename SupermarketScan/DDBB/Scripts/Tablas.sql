USE [SupermarketScan]
GO

SELECT * FROM Categorias;
SELECT * FROM IngestaReferencia;
SELECT * FROM PaisOrigen;
SELECT * FROM Productos;
SELECT * FROM Usuarios;
 
-- Scaffold-DbContext "Server=localhost;Database=SupermarketScan;Uid=sa;Pwd=Santi_r6*!;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir C:\Users\sballestin\source\repos\SupermarketScan\SupermarketScan\Entities\Products\ -Tables Categorias, IngestaReferencia, PaisOrigen, Productos -ContextDir C:\Users\sballestin\source\repos\SupermarketScan\SupermarketScan\DbContext\Products\ -Context ProductsDbContext
-- Scaffold-DbContext "Server=localhost;Database=SupermarketScan;Uid=sa;Pwd=Santi_r6*!;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir C:\Users\sballestin\source\repos\SupermarketScan\SupermarketScan\Entities\Users\ -Tables Usuarios -ContextDir C:\Users\sballestin\source\repos\SupermarketScan\SupermarketScan\DbContext\Users\ -Context UsersDbContext
