namespace CleanArchitecture.Domain.ValueObjects;

public record Address
{
    public Address(string street, string zipCode, string city)
    {
        if (string.IsNullOrWhiteSpace(street))
        {
            throw new ArgumentException($"'{nameof(street)}' cannot be null or whitespace.", nameof(street));
        }
        if (string.IsNullOrWhiteSpace(zipCode))
        {
            throw new ArgumentException($"'{nameof(zipCode)}' cannot be null or whitespace.", nameof(zipCode));
        }
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException($"'{nameof(city)}' cannot be null or whitespace.", nameof(city));
        }

        Street = street;
        ZipCode = zipCode;
        City = city;
    }

    private Address() { }

    public string Street { get; private set; }
    public string ZipCode { get; private set; }
    public string City { get; private set; }
}

