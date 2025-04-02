using Arra.Application.Abstractions.Messaging;

namespace Arra.Application.Users.GetCurrent;

public sealed record GetCurrentUserQuery : IQuery<UserResponse>;
