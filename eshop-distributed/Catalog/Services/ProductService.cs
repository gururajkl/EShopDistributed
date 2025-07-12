namespace Catalog.Services;

public class ProductService(ProductDbContext dbContext)
{
    /// <summary>
    /// Gets all products from the database.
    /// </summary>
    /// <returns>All products.</returns>
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    /// <summary>
    /// Gets a product by its ID from the database.
    /// </summary>
    /// <param name="id">Id of the product.</param>
    /// <returns>Nullable product from the database.</returns>
    public async Task<Product?> GetProductById(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }

    /// <summary>
    /// Creates a new product in the database.
    /// </summary>
    /// <param name="product">Product entity to add to the database.</param>
    public async Task CreateProductAsync(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="updatedProduct">Old product.</param>
    /// <param name="inputProduct">New product.</param>
    public async Task UpdateProductAsync(Product updatedProduct, Product inputProduct)
    {
        updatedProduct.Name = inputProduct.Name;
        updatedProduct.Description = inputProduct.Description;
        updatedProduct.ImageUrl = inputProduct.ImageUrl;
        updatedProduct.Price = inputProduct.Price;

        dbContext.Products.Update(updatedProduct);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a product from the database.
    /// </summary>
    /// <param name="product">Product to delete.</param>
    public async Task DeleteProductAsync(Product product)
    {
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
    }
}
