namespace Catalog.Data;

// DbContext for the Product entity in the Catalog microservice.
public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options) 
{
    /// <summary>
    /// Property that represents the Products table in the database.
    /// </summary>
    public DbSet<Product> Products => Set<Product>();
}
