namespace Basket.Models;

/// <summary>
/// Entity representing a shopping cart for a user.
/// </summary>
public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(t => t.Price * t.Quantity);
}
