using Arra.Application.Abstractions.Messaging;

namespace Arra.Application.Users.UpdateCurrent;

public sealed record UpdateCurrentUserCommand(
    string FirstName,
    string LastName) : ICommand<Guid>;
