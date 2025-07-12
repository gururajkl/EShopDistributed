using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Services;

public class BasketService(IDistributedCache cache)
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
