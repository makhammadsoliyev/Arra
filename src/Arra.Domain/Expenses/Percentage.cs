namespace Arra.Domain.Expenses;

public sealed record Percentage
{
    private Percentage(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; private set; }

    public static Percentage Create(decimal value)
    {
        if (value < 0 || value > 100)
            throw new ApplicationException("Percentage must be between 0 and 100");

        return new Percentage(value);
    }
}
