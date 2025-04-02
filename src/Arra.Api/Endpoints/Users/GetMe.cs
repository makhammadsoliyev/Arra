using Arra.Api.Extensions;
using Arra.Api.Infrastructure;
using Arra.Application.Users.GetCurrent;
using MediatR;

namespace Arra.Api.Endpoints.Users;

internal sealed class GetMe : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/me", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetCurrentUserQuery();

            var result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}