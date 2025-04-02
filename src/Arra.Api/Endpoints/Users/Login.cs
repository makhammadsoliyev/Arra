using Arra.Api.Extensions;
using Arra.Api.Infrastructure;
using Arra.Application.Users.Login;
using MediatR;

namespace Arra.Api.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    internal sealed record Request(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (Request request, ISender sender, CancellationToken CancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            var result = await sender.Send(command, CancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
