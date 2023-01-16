using SO.Domain.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.CustomerService.Domain.Customer;

public class Customer : AuditedAggregateRoot<Guid>
{
    public string? IdentityId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [NotMapped]
    public string FullName => FirstName + " " + LastName;
    public Address? Address { get; set; }
    public string? Nationality { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
}