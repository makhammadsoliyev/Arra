using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Arra.Infrastructure.Authentication;

public sealed class JwtOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<JwtOptions>
{
    public void Configure(JwtOptions options)
    {
        configuration.GetRequiredSection(nameof(JwtOptions)).Bind(options);
    }
}