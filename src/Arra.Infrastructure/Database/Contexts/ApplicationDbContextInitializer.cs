using Arra.Domain.Users;
using Arra.Infrastructure.Database.Options;
using Arra.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Arra.Infrastructure.Database.Contexts;

public sealed class ApplicationDbContextInitializer(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IDateTimeProvider dateTimeProvider,
    IOptions<AdminOptions> adminOptions) : IDisposable
{
    private readonly AdminOptions adminOptions = adminOptions.Value;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task EnsureDefaultAdminAndRolesAsync()
    {
        var accountRoles = Enum.GetNames<AccountRole>();
        foreach (var accountRole in accountRoles)
        {
            var isRoleExists = await roleManager.RoleExistsAsync(accountRole);
            if (!isRoleExists)
                await roleManager.CreateAsync(new Role
                {
                    Name = accountRole,
                    NormalizedName = accountRole.ToUpperInvariant()
                });
        }

        var user = await userManager.FindByEmailAsync(adminOptions.Email);
        if (user is null)
        {
            var newUser = User.Create(
                new FirstName(adminOptions.FirstName),
                new LastName(adminOptions.LastName),
                adminOptions.Email,
                dateTimeProvider.UtcNow);

            await userManager.CreateAsync(newUser, adminOptions.Password);
            await userManager.AddToRolesAsync(newUser, accountRoles);
        }
    }
}
