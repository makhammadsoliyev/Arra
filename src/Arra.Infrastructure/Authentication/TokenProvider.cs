using Arra.Application.Abstractions.Authentication;
using Arra.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Arra.Infrastructure.Authentication;

internal sealed class TokenProvider(
    UserManager<User> userManager,
    IOptions<JwtOptions> jwtOptions,
    JwtSecurityTokenHandler jwtSecurityTokenHandler) : ITokenProvider
{
    private readonly JwtOptions jwtOptions = jwtOptions.Value;

    public async Task<string> CreateAsync(User user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        claims.AddRange(await userManager.GetClaimsAsync(user));
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(jwtOptions.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return jwtSecurityTokenHandler.WriteToken(token);
    }
}
