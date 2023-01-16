using SO.Domain;

namespace SO.CustomerService.Domain.Customer;

public class Address : ValueObject
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Text { get; set; }
    public string? ZipCode { get; set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Text;
        yield return District;
        yield return City;
        yield return Country;
        yield return ZipCode;
    }
}