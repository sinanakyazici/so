using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SO.Shared.Domain.Order;

public class OrderItemSharedModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public Guid ProductTypeId { get; set; }
    public string? ProductTypeName { get; set; }
    public string? ProductTypeDescription { get; set; }

    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public OrderItemSharedModel()
    {
        Id = ObjectId.GenerateNewId();
    }
}
