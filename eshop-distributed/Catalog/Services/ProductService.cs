using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Catalog.Services;

public class ProductService(ProductDbContext dbContext, IBus bus)
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
    public async Task<Product?> GetProductByIdAsync(int id)
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
    /// <param name="existingProduct">Old product.</param>
    /// <param name="inputProduct">New product.</param>
    public async Task UpdateProductAsync(Product existingProduct, Product inputProduct)
    {
        // If price has changed, publish an integration event.
        if (existingProduct.Price != inputProduct.Price)
        {
            // Set updated price info here.
            var integrationEvent = new ProductPriceChangedIntegrationEvent
            {
                ProductId = existingProduct.Id,
                Name = inputProduct.Name,
                Description = inputProduct.Description,
                Price = inputProduct.Price,
                ImageUrl = inputProduct.ImageUrl
            };

            // Publish the integration event to the message bus.
            await bus.Publish(integrationEvent);
        }

        existingProduct.Name = inputProduct.Name;
        existingProduct.Description = inputProduct.Description;
        existingProduct.ImageUrl = inputProduct.ImageUrl;
        existingProduct.Price = inputProduct.Price;

        dbContext.Products.Update(existingProduct);
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
