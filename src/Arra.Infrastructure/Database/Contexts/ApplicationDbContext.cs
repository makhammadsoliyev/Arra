using Arra.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Arra.Infrastructure.Database.Contexts;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<User,
            Role,
            Guid,
            IdentityUserClaim<Guid>,
            UserRole,
            IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>,
            IdentityUserToken<Guid>>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);

        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();

        builder.Entity<User>()
            .Ignore(user => user.AccessFailedCount)
            .Ignore(user => user.LockoutEnabled)
            .Ignore(user => user.LockoutEnd)
            .Ignore(user => user.TwoFactorEnabled)
            .Ignore(user => user.EmailConfirmed)
            .Ignore(user => user.PhoneNumber)
            .Ignore(user => user.PhoneNumberConfirmed);

        builder.Entity<User>().ToTable("users");
        builder.Entity<Role>().ToTable("roles");
        builder.Entity<UserRole>().ToTable("user_roles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
    }
}
