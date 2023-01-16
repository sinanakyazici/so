using SO.Application.Cqrs;

namespace SO.OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommand : ICommand<bool>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string? Notes { get; set; }
    public AddressDto? Address { get; set; }

    public IEnumerable<OrderItemDto> OrderItems { get; set; }

    public CreateOrderCommand()
    {
        OrderItems = new List<OrderItemDto>();
    }

    public record OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    public record AddressDto
    {
        public string? Text { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
    }
}