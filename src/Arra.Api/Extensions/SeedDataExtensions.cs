using Arra.Infrastructure.Database.Contexts;

namespace Arra.Api.Extensions;

internal static class SeedDataExtensions
{
    public static async Task<IApplicationBuilder> EnsureDefaultAdminAndRolesAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var initializer =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.EnsureDefaultAdminAndRolesAsync();

        return app;
    }
}