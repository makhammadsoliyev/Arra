using Arra.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Arra.Api.Extensions;

internal static class MigrationExtensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var context =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();

        return app;
    }
}
