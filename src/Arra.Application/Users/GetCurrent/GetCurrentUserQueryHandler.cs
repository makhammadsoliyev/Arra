using Arra.Application.Abstractions.Authentication;
using Arra.Application.Abstractions.Messaging;
using Arra.Domain.Users;
using Arra.SharedKernel;

namespace Arra.Application.Users.GetCurrent;

internal sealed class GetCurrentUserQueryHandler(IIdentityService identityService, IUserContext userContext)
    : IQueryHandler<GetCurrentUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if (userContext.UserId == default)
            return Result.Failure<UserResponse>(UserErrors.Unauthorized());

        var result = await identityService.GetUserByIdAsync(userContext.UserId, cancellationToken);
        if (!result.IsSuccess)
            return Result.Failure<UserResponse>(result.Error);

        var user = new UserResponse(result.Value);

        return user;
    }
}
