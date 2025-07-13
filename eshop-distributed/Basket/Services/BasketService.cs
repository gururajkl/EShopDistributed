using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Services;

public class BasketService(IDistributedCache cache, CatalogApiClient catalogApiClient)
{
    /// <summary>
    /// Get basket from the redis cache.
    /// </summary>
    /// <param name="userName">Key to find.</param>
    /// <returns><see cref="ShoppingCart"/> with data.</returns>
    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket = await cache.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket) ? null : JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    /// <summary>
    /// Updates the value in redis.
    /// </summary>
    /// <param name="cart">Item to update.</param>
    public async Task UpdateBasket(ShoppingCart cart)
    {
        // Before updating the basket, we can enrich it with product details,
        // By calling the catalog service using catalogApiClient.
        foreach (var cartItem in cart.Items)
        {
            var product = await catalogApiClient.GetProductByIdAsync(cartItem.ProductId);
            if (product is not null)
            {
                cartItem.ProductName = product.Name;
                cartItem.Price = product.Price;
            }
        }

        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));
    }

    /// <summary>
    /// Delete the key-value from redis.
    /// </summary>
    /// <param name="userName">Key to delete.</param>
    public async Task DeleteBasket(string userName)
    {
        await cache.RemoveAsync(userName);
    }
}
