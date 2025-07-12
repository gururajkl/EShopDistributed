namespace Basket.Models;

/// <summary>
/// Entity representing an item in a shopping cart.
/// </summary>
public class ShoppingCartItem
{
    public int ProductId { get; set; } = default!;
    public string Color { get; set; } = default!;
    public int Quantity { get; set; } = default!;

    // These comes from the Catalog module.
    public decimal Price { get; set; } = default!;
    public string ProductName { get; set; } = default!;
}
