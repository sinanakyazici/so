using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SO.Domain;

namespace SO.Shared.Domain.Order;

[BsonIgnoreExtraElements]
[CollectionName("orders")]
public class OrderSharedModel : ISharedModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal OrderTotalPrice { get; set; }
    public string? Notes { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Text { get; set; }
    public string? ZipCode { get; set; }
    public DateTime CreationTime { get; set; }

    public Guid CustomerId { get; set; }
    public string? IdentityId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }



    public OrderItemSharedModel[]? OrderItems { get; set; }

    public OrderSharedModel()
    {
        Id = ObjectId.GenerateNewId();
    }
}