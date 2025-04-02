using Arra.Api.Extensions;
using Arra.Api.Infrastructure;
using Arra.Application.Users.Register;
using MediatR;

namespace Arra.Api.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    internal sealed record Request(string FirstName, string LastName, string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (Request request, ISender sender, CancellationToken CancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var result = await sender.Send(command, CancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);

        })
        .WithTags(Tags.Users);
    }
}
