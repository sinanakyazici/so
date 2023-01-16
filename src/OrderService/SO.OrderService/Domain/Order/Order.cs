using SO.Domain.Auditing;

namespace SO.OrderService.Domain.Order;

public class Order : AuditedAggregateRoot<Guid>
{
    public Guid CustomerId { get; set; }
    public decimal OrderTotalPrice { get; set; }
    public string? Notes { get; set; }
    
    public Address? Address { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}