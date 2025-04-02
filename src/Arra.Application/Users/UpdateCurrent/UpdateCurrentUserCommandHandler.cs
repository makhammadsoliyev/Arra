using Arra.Application.Abstractions.Authentication;
using Arra.Application.Abstractions.Messaging;
using Arra.Domain.Users;
using Arra.SharedKernel;

namespace Arra.Application.Users.UpdateCurrent;

internal sealed class UpdateCurrentUserCommandHandler(
    IIdentityService identityService,
    IUserContext userContext)
    : ICommandHandler<UpdateCurrentUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
    {
        if (userContext.UserId == Guid.Empty)
            return Result.Failure<Guid>(UserErrors.Unauthorized());

        return await identityService.UpdateAsync(
            userContext.UserId,
            request.FirstName,
            request.LastName,
            cancellationToken);
    }
}
