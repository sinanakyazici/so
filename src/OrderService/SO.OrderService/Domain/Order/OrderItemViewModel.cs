namespace SO.OrderService.Domain.Order;

public class OrderItemViewModel
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public string? LastModifierName { get; set; }
    public DateTime CreationTime { get; set; }
    public string CreatorName { get; set; } = null!;
    public DateTime? ValidFor { get; set; }
}