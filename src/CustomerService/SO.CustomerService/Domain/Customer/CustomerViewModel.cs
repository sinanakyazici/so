namespace SO.CustomerService.Domain.Customer;

public class CustomerViewModel
{
    public Guid Id { get; set; }
    public string? IdentityId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName => FirstName + " " + LastName;
    public AddressViewModel? Address { get; set; }
    public string? Nationality { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public string? LastModifierName { get; set; }
    public DateTime CreationTime { get; set; }
    public string CreatorName { get; set; } = null!;
    public DateTime? ValidFor { get; set; }
}