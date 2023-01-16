using SO.Domain.Auditing;

namespace SO.OrderService.Domain.Order;

public class OrderItem : AuditedEntity<Guid>
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public Order? Order { get; set; }
}