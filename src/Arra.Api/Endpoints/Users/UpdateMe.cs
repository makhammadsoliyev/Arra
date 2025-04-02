using Arra.Api.Extensions;
using Arra.Api.Infrastructure;
using Arra.Application.Users.UpdateCurrent;
using MediatR;

namespace Arra.Api.Endpoints.Users;

internal sealed class UpdateMe : IEndpoint
{
    internal sealed record Request(string FirstName, string LastName, string Email, string Password);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/me", async (Request request, ISender sender, CancellationToken CancellationToken) =>
        {
            var command = new UpdateCurrentUserCommand(request.FirstName, request.LastName);

            var result = await sender.Send(command, CancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}