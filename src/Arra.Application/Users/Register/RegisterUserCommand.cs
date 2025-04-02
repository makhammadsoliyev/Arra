﻿using Arra.Application.Abstractions.Messaging;

namespace Arra.Application.Users.Register;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<Guid>;
