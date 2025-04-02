﻿using Arra.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace Arra.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");

    public static Error NotFoundByEmail() => Error.NotFound(
        "Users.NotFoundByEmail",
        "The user with the specified email was not found");

    public static Error EmailNotUnique() => Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");

    public static Error InvalidCredationals() => Error.Unauthorized(
        "Users.InvalidCredationals",
        "Invalid email or password");

    public static Error FromIdentityErrors(IEnumerable<IdentityError> errors) => Error.Unauthorized(
        "Users.IdentityErrors", string.Join(", ", errors.Select(e => e.Description)));
}
