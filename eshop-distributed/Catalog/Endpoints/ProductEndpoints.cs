namespace Catalog.Endpoints;

public static class ProductEndpoints
{
    /// <summary>
    /// Creates minimal endpoints for product operations.
    /// </summary>
    /// <param name="app"></param>
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products");

        // GET all products.
        group.MapGet("/", async (ProductService service) =>
        {
            var products = await service.GetAllProductsAsync();
            return Results.Ok(products);
        }).WithName("GetAllProducts").Produces<List<Product>>(StatusCodes.Status200OK);

        // GET product by Id.
        group.MapGet("/{id}", async (int id, ProductService service) =>
        {
            var product = await service.GetProductByIdAsync(id);

            if (product is null) return Results.NotFound();

            return Results.Ok(product);
        }).WithName("GetProductById").Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // CREATE product.
        group.MapPost("/", async (Product product, ProductService service) =>
        {
            await service.CreateProductAsync(product);
            return Results.Created($"/products/{product.Id}", product);
        }).WithName("CreateProduct").Produces<Product>(StatusCodes.Status200OK);

        // UPDATE product.
        group.MapPut("/{id}", async (int id, Product inputProduct, ProductService service) =>
        {
            var existingProduct = await service.GetProductByIdAsync(id);

            if (existingProduct is null) return Results.NotFound();

            await service.UpdateProductAsync(existingProduct, inputProduct);

            return Results.NoContent();
        }).WithName("UpdateProduct").Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        // DELETE product.
        group.MapDelete("/{id}", async (int id, ProductService service) =>
        {
            var product = await service.GetProductByIdAsync(id);

            if (product is null) return Results.NotFound();

            await service.DeleteProductAsync(product);

            return Results.Ok();
        }).WithName("DeleteProduct").Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
