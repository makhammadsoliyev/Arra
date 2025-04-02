using Arra.Domain.Users;

namespace Arra.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    Task<string> CreateAsync(User user);
}