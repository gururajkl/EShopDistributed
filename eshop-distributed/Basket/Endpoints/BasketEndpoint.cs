namespace Basket.Endpoints;

public static class BasketEndpoint
{
    // Extension method which includes all the endpoints of the basket operations.
    public static void MapBasketEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("basket");

        group.MapGet("/{userName}", async (string userName, BasketService service) =>
        {
            var basket = await service.GetBasket(userName);

            if (basket is null) return Results.NotFound();

            return Results.Ok(basket);
        }).WithName("GetBasket").Produces<ShoppingCart>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound).RequireAuthorization();

        group.MapPost("/", async (ShoppingCart cart, BasketService service) =>
        {
            await service.UpdateBasket(cart);
            return Results.Created("GetBasket", cart);
        }).WithName("UpdateBasket").Produces(StatusCodes.Status201Created).RequireAuthorization();

        group.MapDelete("/{userName}", async (string userName, BasketService service) =>
        {
            await service.DeleteBasket(userName);
            return Results.NoContent();
        }).WithName("DeleteBasket").Produces(StatusCodes.Status204NoContent).RequireAuthorization();
    }
}
