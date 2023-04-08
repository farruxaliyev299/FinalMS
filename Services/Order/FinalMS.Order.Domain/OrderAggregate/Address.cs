using FinalMS.Order.Domain.Core;

namespace FinalMS.Order.Domain.OrderAggregate;

public class Address: ValueObject
{
    public string Province { get; private set; }

    public string District { get; private set; }

    public string Street { get; private set; }

    public string Line { get; private set; }

    public Address(string province, string district, string street, string line)
    {
        Province = province;
        District = district;
        Street = street;
        Line = line;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Province;
        yield return District;
        yield return Street;
        yield return Line;
    }
}
