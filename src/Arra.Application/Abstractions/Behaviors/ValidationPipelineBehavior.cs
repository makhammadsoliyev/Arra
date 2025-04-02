using Arra.Application.Abstractions.Messaging;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = Arra.Application.Exceptions.ValidationException;

namespace Arra.Application.Abstractions.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IBaseCommand

{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var errors = await ValidateAsync(request, cancellationToken);

        if (errors.Any())
            throw ValidationException.FromValidationFailures(errors);

        return await next();
    }

    private async Task<List<ValidationFailure>> ValidateAsync(TRequest request, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return [];

        var validationTasks = validators.Select(validator => validator.ValidateAsync(request, cancellationToken));
        var validationResults = await Task.WhenAll(validationTasks);

        return validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();
    }
}
