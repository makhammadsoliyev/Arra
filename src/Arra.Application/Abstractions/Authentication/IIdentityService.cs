using Arra.Domain.Users;
using Arra.SharedKernel;

namespace Arra.Application.Abstractions.Authentication;

public interface IIdentityService
{
    Task<Result<Guid>> RegisterAsync(
        string firstName,
        string LastName,
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<Result<string>> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<Result<User>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Result<User>> GetUserByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<Result<Guid>> UpdateAsync(
        Guid id,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);
}
