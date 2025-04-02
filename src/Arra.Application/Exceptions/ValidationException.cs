using Arra.SharedKernel;
using FluentValidation.Results;

namespace Arra.Application.Exceptions;

public sealed class ValidationException : Exception
{
    private ValidationException(string code, Error[] errors)
        : base("One or more validation errors occured")
    {
        Errors = errors;
        Code = code;
    }

    public Error[] Errors { get; private set; }

    public string Code { get; private set; }

    public static ValidationException FromValidationFailures(List<ValidationFailure> validationFailures) =>
        new("Validation.General", validationFailures.Select(f => new Error(f.ErrorCode, f.ErrorMessage, ErrorType.Validation)).ToArray());
}
