﻿namespace Basket.ApiClients;

// API client for the Catalog microservice helps to consume the catalog services.
public class CatalogApiClient(HttpClient httpClient)
{
    public async Task<Product> GetProductByIdAsync(int id)
    {
        var response = await httpClient.GetFromJsonAsync<Product>($"/products/{id}");
        return response!;
    }
}
