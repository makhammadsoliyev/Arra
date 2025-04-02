using Arra.Application.Abstractions.Authentication;
using Arra.Domain.Users;
using Arra.Infrastructure.Authentication;
using Arra.Infrastructure.Database.Contexts;
using Arra.Infrastructure.Database.Options;
using Arra.Infrastructure.Time;
using Arra.SharedKernel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Arra.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddAuthenticationInternal()
            .AddAuthorization();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseSqlServer(connectionString)
                .UseSnakeCaseNamingConvention());

        services.AddScoped<ApplicationDbContextInitializer>();
        services.ConfigureOptions<AdminOptionsSetup>();

        return services;
    }

    public static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddHttpContextAccessor();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<JwtSecurityTokenHandler>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
