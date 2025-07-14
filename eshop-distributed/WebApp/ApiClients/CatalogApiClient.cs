using Catalog.Models;

namespace WebApp.ApiClients;

public class CatalogApiClient(HttpClient client)
{
    public async Task<List<Product>> GetProducts()
    {
        var response = await client.GetFromJsonAsync<List<Product>>("/products");
        return response!;
    }

    public async Task<Product> GetProductById(int id)
    {
        var response = await client.GetFromJsonAsync<Product>($"/products/{id}");
        return response!;
    }
}
