using Arra.Domain.Users;

namespace Arra.Application.Users.GetById;

public sealed record UserResponse
{
    public UserResponse(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName.Value;
        LastName = user.LastName.Value;
        Email = user.Email;
    }

    public Guid Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }
}
