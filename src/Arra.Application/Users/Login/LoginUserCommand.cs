using Arra.Application.Abstractions.Messaging;

namespace Arra.Application.Users.Login;

public sealed record LoginUserCommand(
    string Email,
    string Password) : ICommand<string>;
