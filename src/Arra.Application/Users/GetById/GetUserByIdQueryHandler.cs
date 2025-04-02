using Arra.Application.Abstractions.Authentication;
using Arra.Application.Abstractions.Messaging;
using Arra.SharedKernel;

namespace Arra.Application.Users.GetById;

internal sealed class GetUserByIdQueryHandler(IIdentityService identityService)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await identityService.GetUserByIdAsync(request.Id, cancellationToken);
        if (!result.IsSuccess)
            return Result.Failure<UserResponse>(result.Error);

        var user = new UserResponse(result.Value);

        return user;
    }
}
