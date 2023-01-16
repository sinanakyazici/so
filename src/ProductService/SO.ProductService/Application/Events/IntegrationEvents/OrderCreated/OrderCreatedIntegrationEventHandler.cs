using MassTransit;
using MongoDB.Bson;
using MongoDB.Driver;
using SO.ProductService.Domain.Product;
using SO.Shared.Domain.Order;
using SO.Shared.Domain.Order.Events;

namespace SO.ProductService.Application.Events.IntegrationEvents.OrderCreated;

public class OrderCreatedIntegrationEventHandler : IConsumer<OrderCreatedIntegrationEvent>
{
    private readonly ILogger<OrderCreatedIntegrationEventHandler> _logger;
    private readonly IProductQueryRepository _productQueryRepository;
    private readonly IOrderSharedRepository _orderSharedRepository;

    public OrderCreatedIntegrationEventHandler(ILogger<OrderCreatedIntegrationEventHandler> logger, IProductQueryRepository productQueryRepository, IOrderSharedRepository orderSharedRepository)
    {
        _logger = logger;
        _productQueryRepository = productQueryRepository;
        _orderSharedRepository = orderSharedRepository;
    }

    public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
    {
        var productViewModels = await _productQueryRepository.GetProducts();
        foreach (var productViewModel in productViewModels)
        {
            var filter = Builders<OrderSharedModel>.Filter.Where(x => x.OrderId == context.Message.OrderId);
            var update = Builders<OrderSharedModel>.Update
                .Set("OrderItems.$[i].ProductTypeId", productViewModel.ProductTypeId)
                .Set("OrderItems.$[i].ProductCode", productViewModel.ProductCode)
                .Set("OrderItems.$[i].ProductTypeName", productViewModel.ProductTypeName)
                .Set("OrderItems.$[i].ProductTypeDescription", productViewModel.ProductTypeDescription);

            var arrayFilters = new[]
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("i.ProductId", new BsonDocument("$in", new BsonArray(new [] { productViewModel.Id })))),
            };
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
            await _orderSharedRepository.UpdateOrder(filter, update, updateOptions);
        }
    }
}