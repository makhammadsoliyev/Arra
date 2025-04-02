using Arra.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Arra.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor)
    : IUserContext
{
    public Guid UserId => httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub) is { } userId
        ? Guid.Parse(userId)
        : Guid.Empty;
}