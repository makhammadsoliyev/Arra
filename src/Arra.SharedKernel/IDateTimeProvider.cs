namespace Arra.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
