namespace SO.OrderService.Domain.Order;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal OrderTotalPrice { get; set; }
    public string? Notes { get; set; }
    public AddressViewModel? Address { get; set; }
    public DateTime CreationTime { get; set; }

    public ICollection<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
}