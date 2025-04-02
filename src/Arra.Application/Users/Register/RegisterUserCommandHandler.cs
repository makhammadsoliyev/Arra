using Arra.Application.Abstractions.Authentication;
using Arra.Application.Abstractions.Messaging;
using Arra.SharedKernel;

namespace Arra.Application.Users.Register;

internal sealed class RegisterUserCommandHandler(IIdentityService identityService)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.RegisterAsync(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            cancellationToken);
    }
}