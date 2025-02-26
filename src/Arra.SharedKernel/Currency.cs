namespace Arra.SharedKernel;

public sealed record Currency
{
    internal static readonly Currency None = new("");
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Sum = new("SUM");

    private Currency(string code) => Code = code;

    public string Code { get; init; }

    public static Result<Currency> FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
            throw new ApplicationException("The currency code invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All =
    [
        Usd,
        Sum,
    ];
}
