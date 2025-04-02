using Arra.Application.Abstractions.Authentication;
using Arra.Application.Abstractions.Messaging;
using Arra.SharedKernel;

namespace Arra.Application.Users.Login;

internal sealed class LoginUserCommandHandler(IIdentityService identityService)
    : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.LoginAsync(
            request.Email,
            request.Password,
            cancellationToken);
    }
}
