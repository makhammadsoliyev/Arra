using Arra.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace Arra.Domain.Users;

public sealed class User : IdentityUser<Guid>, IEntity
{
    public DateOnly CreatedOnUtc { get; private set; }

    public static User Create(
        string email,
        DateOnly createdOnUtc)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = email,
            CreatedOnUtc = createdOnUtc
        };

        return newUser;
    }
}
