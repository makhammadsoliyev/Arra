using Arra.Application.Abstractions.Authentication;
using Arra.Domain.Users;
using Arra.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace Arra.Infrastructure.Authentication;

internal sealed class IdentityService(
    UserManager<User> userManager,
    ITokenProvider tokenProvider,
    IDateTimeProvider dateTimeProvider)
    : IIdentityService
{
    public async Task<Result<Guid>> RegisterAsync(
        string firstName,
        string LastName,
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var existUser = await userManager.FindByEmailAsync(email);
        if (existUser != null)
            return Result.Failure<Guid>(UserErrors.EmailNotUnique());

        var user = User.Create(
            new FirstName(firstName),
            new LastName(LastName),
            email,
            dateTimeProvider.UtcNow);

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return Result.Failure<Guid>(UserErrors.FromIdentityErrors(result.Errors));

        await userManager.AddToRoleAsync(user, nameof(AccountRole.User));

        return user.Id;
    }

    public async Task<Result<string>> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return Result.Failure<string>(UserErrors.InvalidCredationals());

        var result = await userManager.CheckPasswordAsync(user, password);
        if (!result)
            return Result.Failure<string>(UserErrors.InvalidCredationals());

        return await tokenProvider.CreateAsync(user);
    }

    public async Task<Result<User>> GetUserByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return Result.Failure<User>(UserErrors.NotFoundByEmail());

        return user;
    }

    public async Task<Result<User>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return Result.Failure<User>(UserErrors.NotFound(id));

        return user;
    }

    public async Task<Result<Guid>> UpdateAsync(
        Guid id,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return Result.Failure<Guid>(UserErrors.NotFound(id));

        user.Update(
            new FirstName(firstName),
            new LastName(lastName));

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return Result.Failure<Guid>(UserErrors.FromIdentityErrors(result.Errors));

        return user.Id;
    }
}
