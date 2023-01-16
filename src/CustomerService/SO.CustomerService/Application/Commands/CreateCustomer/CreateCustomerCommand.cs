using SO.Application.Cqrs;

namespace SO.CustomerService.Application.Commands.CreateCustomer;

public class CreateCustomerCommand : ICommand<bool>
{
    public Guid Id { get; set; }
    public string? IdentityId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName => FirstName + " " + LastName;
    public AddressDto? Address { get; set; }
    public string? Nationality { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? PhoneNumber { get; set; }

    public record AddressDto
    {
        public string? Text { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
    }
}