using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Basket.EventHandlers;

/// <summary>
/// Consumes the ProductPriceChangedIntegrationEvent and updates the basket item price.
/// </summary>
/// <param name="service">Basket service object.</param>
public class ProductPriceChangedIntegrationEventHandler(BasketService service) :
    IConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        // Find the basket and update the product price.
        await service.UpdateBasketItemProductPrice(context.Message.ProductId, context.Message.Price);
    }
}
