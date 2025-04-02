using Arra.Application.Abstractions.Authentication;
using Arra.Application.Abstractions.Messaging;
using Arra.SharedKernel;

namespace Arra.Application.Users.GetByEmail;

internal sealed class GetUserByEmailQueryHandler(IIdentityService identityService)
    : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var result = await identityService.GetUserByEmailAsync(request.Email, cancellationToken);
        if (!result.IsSuccess)
            return Result.Failure<UserResponse>(result.Error);

        var user = new UserResponse(result.Value);

        return user;
    }
}
