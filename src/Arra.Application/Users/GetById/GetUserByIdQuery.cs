using Arra.Application.Abstractions.Messaging;

namespace Arra.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<UserResponse>;
