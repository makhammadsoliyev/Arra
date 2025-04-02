using Arra.Application.Abstractions.Messaging;

namespace Arra.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
