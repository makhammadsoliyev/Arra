using Arra.SharedKernel;

namespace Arra.Api.Infrastructure;

internal static class CustomResults
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Result is successful");

        var (title, detail, type, statusCode) = GetErrorDetails(result.Error);

        return Results.Problem(
            title: title,
            detail: detail,
            type: type,
            statusCode: statusCode);
    }

    private static (string Title, string Detail, string Type, int StatusCode) GetErrorDetails(Error error)
    {
        return error.Type switch
        {
            ErrorType.Failure => (error.Code, error.Description, "https://httpstatuses.com/400", StatusCodes.Status400BadRequest),
            ErrorType.Validation => (error.Code, error.Description, "https://httpstatuses.com/400", StatusCodes.Status400BadRequest),
            ErrorType.NotFound => (error.Code, error.Description, "https://httpstatuses.com/404", StatusCodes.Status404NotFound),
            ErrorType.Conflict => (error.Code, error.Description, "https://httpstatuses.com/409", StatusCodes.Status409Conflict),
            ErrorType.Unauthorized => (error.Code, error.Description, "https://httpstatuses.com/401", StatusCodes.Status401Unauthorized),
            _ => ("Internal Server Error", "An unexpected error occurred", "https://httpstatuses.com/500", StatusCodes.Status500InternalServerError)
        };
    }
}
