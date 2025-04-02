using FluentValidation;

namespace Arra.Application.Users.UpdateCurrent;

internal sealed class UpdateCurrentUserCommandValidator
    : AbstractValidator<UpdateCurrentUserCommand>
{
    public UpdateCurrentUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
