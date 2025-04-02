
using Arra.Api.Extensions;
using Arra.Api.Infrastructure;
using Arra.Application.Users.GetByEmail;
using MediatR;

namespace Arra.Api.Endpoints.Users;

internal sealed class GetByEmail : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{email}", async (string email, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetUserByEmailQuery(email);

            var result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
