using Arra.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace Arra.Domain.Users;

public sealed class User : IdentityUser<Guid>, IEntity
{
    private User()
    { }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public static User Create(
        FirstName firstName,
        LastName lastName,
        string email,
        DateTime createdOnUtc)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            NormalizedEmail = email.ToUpperInvariant(),
            UserName = email,
            NormalizedUserName = email.ToUpperInvariant(),
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedOnUtc = createdOnUtc
        };

        return newUser;
    }

    public void Update(
        FirstName firstName,
        LastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
