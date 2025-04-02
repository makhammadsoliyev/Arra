using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Arra.Infrastructure.Authentication;

public sealed class IdentityOptionsSetup : IConfigureOptions<IdentityOptions>
{
    public void Configure(IdentityOptions options)
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
    }
}
